import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { usePlanets } from '@/hooks/usePlanets';
import { PlanetCard } from '@/components/PlanetCard';
import { LoadingSpinner } from '@/components/LoadingSpinner';
import { ErrorMessage } from '@/components/ErrorMessage';
import { CreatePlanetModal } from '@/components/CreatePlanetModal';
import type { Planet } from '@/types';

export function PlanetsPage() {
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const navigate = useNavigate();
  const { data: planets, isLoading, error, refetch } = usePlanets();

  const handlePlanetClick = (planet: Planet) => {
    navigate(`/planets/${planet.id}`);
  };

  if (isLoading) {
    return (
      <div className="container mx-auto px-4 py-16">
        <LoadingSpinner />
      </div>
    );
  }

  if (error) {
    return (
      <div className="container mx-auto px-4 py-16">
        <ErrorMessage
          message="Erro ao carregar planetas"
          onRetry={refetch}
        />
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <div className="mb-8 flex items-center justify-between">
        <div>
          <h1 className="text-4xl font-bold text-gray-900 dark:text-white">
            Planetas
          </h1>
          <p className="mt-2 text-lg text-gray-600 dark:text-gray-300">
            Explore os planetas e descubra as missões disponíveis
          </p>
        </div>

        <button
          onClick={() => setIsCreateModalOpen(true)}
          className="cursor-pointer rounded-lg bg-blue-600 px-6 py-3 font-semibold text-white transition-colors hover:bg-blue-700 dark:bg-blue-500 dark:hover:bg-blue-600"
        >
          + Criar Planeta
        </button>
      </div>

      {planets && Array.isArray(planets) && planets.length > 0 ? (
        <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
          {planets.map((planet) => (
            <PlanetCard
              key={planet.id}
              planet={planet}
              onClick={handlePlanetClick}
            />
          ))}
        </div>
      ) : (
        <div className="rounded-lg border border-gray-200 bg-white p-12 text-center dark:border-gray-700 dark:bg-slate-800">
          <p className="text-gray-600 dark:text-gray-300">
            Nenhum planeta encontrado
          </p>
        </div>
      )}

      <CreatePlanetModal
        isOpen={isCreateModalOpen}
        onClose={() => setIsCreateModalOpen(false)}
      />
    </div>
  );
}
