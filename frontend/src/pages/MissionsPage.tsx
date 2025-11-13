import { useState } from 'react';
import { useMissions } from '@/hooks/useMissions';
import { MissionCard } from '@/components/MissionCard';
import { LoadingSpinner } from '@/components/LoadingSpinner';
import { ErrorMessage } from '@/components/ErrorMessage';
import { CreateMissionModal } from '@/components/CreateMissionModal';
import { useNavigate } from 'react-router-dom';
import type { Mission } from '@/types';

export function MissionsPage() {
  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const { missions, isLoading, error } = useMissions();
  const navigate = useNavigate();

  const handleMissionSelect = (mission: Mission) => {
    navigate(`/missions/${mission.slug}`);
  };

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
        <ErrorMessage message="Erro ao carregar missões" />
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <div className="mb-8 flex items-center justify-between">
        <div>
          <h1 className="text-4xl font-bold text-gray-900 dark:text-white">Missões</h1>
          <p className="mt-2 text-gray-600 dark:text-gray-300">
            Explore todas as missões disponíveis no OrionDev
          </p>
        </div>

        <button
          onClick={() => setIsCreateModalOpen(true)}
          className="cursor-pointer rounded-lg bg-blue-600 px-6 py-3 font-semibold text-white transition-colors hover:bg-blue-700 dark:bg-blue-500 dark:hover:bg-blue-600"
        >
          + Criar Missão
        </button>
      </div>

      {missions && Array.isArray(missions) && missions.length > 0 ? (
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          {missions.map((mission) => (
            <MissionCard
              key={mission.id}
              mission={mission}
              onSelect={handleMissionSelect}
            />
          ))}
        </div>
      ) : (
        <div className="rounded-lg border border-gray-200 bg-white p-12 text-center dark:border-gray-700 dark:bg-slate-800">
          <p className="text-lg text-gray-600 dark:text-gray-300">
            Nenhuma missão encontrada
          </p>
        </div>
      )}

      <CreateMissionModal
        isOpen={isCreateModalOpen}
        onClose={() => setIsCreateModalOpen(false)}
      />
    </div>
  );
}
