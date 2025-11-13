import { useState } from 'react';
import { useSolarSystems } from '@/hooks/useSolarSystems';
import { LoadingSpinner } from '@/components/LoadingSpinner';
import { ErrorMessage } from '@/components/ErrorMessage';
import { CreateSolarSystemModal } from '@/components/CreateSolarSystemModal';

export function SolarSystemsPage() {
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const { solarSystems, isLoading, error } = useSolarSystems();

  if (isLoading) {
    return (
      <div className="container mx-auto px-4 py-8">
        <LoadingSpinner />
      </div>
    );
  }

  if (error) {
    return (
      <div className="container mx-auto px-4 py-8">
        <ErrorMessage message="Erro ao carregar sistemas solares" />
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <div className="mb-8 flex items-center justify-between">
        <div>
          <h1 className="text-4xl font-bold text-gray-900 dark:text-white">Sistemas Solares</h1>
          <p className="mt-2 text-gray-600 dark:text-gray-300">
            Explore o universo do OrionDev
          </p>
        </div>
        
        <button
          onClick={() => setIsCreateModalOpen(true)}
          className="cursor-pointer rounded-lg bg-blue-600 px-6 py-3 font-semibold text-white transition-colors hover:bg-blue-700 dark:bg-blue-500 dark:hover:bg-blue-600"
        >
          + Criar Sistema Solar
        </button>
      </div>

      {solarSystems && Array.isArray(solarSystems) && solarSystems.length > 0 ? (
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          {solarSystems.map((system) => (
            <div
              key={system.id}
              className="rounded-lg border border-gray-200 bg-white p-6 shadow-sm transition-all hover:shadow-md dark:border-gray-700 dark:bg-slate-800"
            >
              <div className="mb-4 text-4xl">🌌</div>
              <h3 className="text-xl font-bold text-gray-900 dark:text-white">{system.name}</h3>
              <p className="mt-2 text-sm text-gray-500 dark:text-gray-400">
                Criado em {new Date(system.createdOn).toLocaleDateString('pt-BR')}
              </p>
            </div>
          ))}
        </div>
      ) : (
        <div className="rounded-lg border border-gray-200 bg-white p-12 text-center dark:border-gray-700 dark:bg-slate-800">
          <p className="text-lg text-gray-600 dark:text-gray-300">
            Nenhum sistema solar encontrado
          </p>
        </div>
      )}

      <CreateSolarSystemModal
        isOpen={isCreateModalOpen}
        onClose={() => setIsCreateModalOpen(false)}
      />
    </div>
  );
}
