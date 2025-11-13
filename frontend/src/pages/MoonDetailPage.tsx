import { useParams, useNavigate } from 'react-router-dom';
import { useMoon } from '@/hooks/useMoons';
import { useMissions } from '@/hooks/useMissions';
import { MissionCard } from '@/components/MissionCard';
import { LoadingSpinner } from '@/components/LoadingSpinner';
import { ErrorMessage } from '@/components/ErrorMessage';
import type { Mission } from '@/types';

export function MoonDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  
  const { data: moon, isLoading: moonLoading, error: moonError, refetch: refetchMoon } = useMoon(id!);
  const { missions, isLoading: missionsLoading } = useMissions({ moonId: id });

  const handleMissionClick = (mission: Mission) => {
    navigate(`/missions/${mission.slug}`);
  };

  if (moonLoading) {
    return (
      <div className="container mx-auto px-4 py-16">
        <LoadingSpinner />
      </div>
    );
  }

  if (moonError || !moon) {
    return (
      <div className="container mx-auto px-4 py-16">
        <ErrorMessage
          message="Erro ao carregar detalhes da lua"
          onRetry={refetchMoon}
        />
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      {/* Header da Lua */}
      <div className="mb-8 rounded-lg border border-gray-200 bg-white p-8 shadow-sm dark:border-gray-700 dark:bg-slate-800">
        <div className="flex items-start gap-6">
          {moon.appearance?.iconPath ? (
            <img
              src={moon.appearance.iconPath}
              alt={moon.name}
              className="h-20 w-20 rounded-full object-cover"
            />
          ) : (
            <div className="flex h-20 w-20 items-center justify-center rounded-full bg-gradient-to-br from-gray-400 to-gray-600 text-3xl font-bold text-white">
              {moon.name[0]}
            </div>
          )}
          
          <div className="flex-1">
            <h1 className="text-4xl font-bold text-gray-900 dark:text-white">
              {moon.name}
            </h1>
            <p className="mt-4 text-lg text-gray-600 dark:text-gray-300">
              {moon.lore}
            </p>
          </div>
        </div>
      </div>

      {/* Missões da Lua */}
      <div>
        <h2 className="mb-4 text-2xl font-bold text-gray-900 dark:text-white">
          🚀 Missões Disponíveis
        </h2>
        {missionsLoading ? (
          <LoadingSpinner />
        ) : missions && missions.length > 0 ? (
          <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
            {Array.isArray(missions) && missions.map((mission: Mission) => (
              <MissionCard
                key={mission.id}
                mission={mission}
                onSelect={handleMissionClick}
              />
            ))}
          </div>
        ) : (
          <div className="rounded-lg border border-gray-200 bg-white p-12 text-center dark:border-gray-700 dark:bg-slate-800">
            <p className="text-gray-600 dark:text-gray-300">
              Nenhuma missão disponível nesta lua
            </p>
          </div>
        )}
      </div>
    </div>
  );
}
