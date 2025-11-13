import { useParams, useNavigate } from 'react-router-dom';
import { usePlanet } from '@/hooks/usePlanets';
import { useMoonsByPlanet } from '@/hooks/useMoons';
import { useMissions } from '@/hooks/useMissions';
import { MoonCard } from '@/components/MoonCard';
import { MissionCard } from '@/components/MissionCard';
import { LoadingSpinner } from '@/components/LoadingSpinner';
import { ErrorMessage } from '@/components/ErrorMessage';
import type { Moon, Mission } from '@/types';

export function PlanetDetailPage() {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  
  const { data: planet, isLoading: planetLoading, error: planetError, refetch: refetchPlanet } = usePlanet(id!);
  const { data: moons, isLoading: moonsLoading } = useMoonsByPlanet(id!);
  const { missions, isLoading: missionsLoading } = useMissions({ planetId: id });

  const handleMoonClick = (moon: Moon) => {
    navigate(`/moons/${moon.id}`);
  };

  const handleMissionClick = (mission: Mission) => {
    navigate(`/missions/${mission.slug}`);
  };

  if (planetLoading) {
    return (
      <div className="container mx-auto px-4 py-16">
        <LoadingSpinner />
      </div>
    );
  }

  if (planetError || !planet) {
    return (
      <div className="container mx-auto px-4 py-16">
        <ErrorMessage
          message="Erro ao carregar detalhes do planeta"
          onRetry={refetchPlanet}
        />
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      {/* Header do Planeta */}
      <div className="mb-8 rounded-lg border border-gray-200 bg-white p-8 shadow-sm dark:border-gray-700 dark:bg-slate-800">
        <div className="flex items-start gap-6">
          {planet.appearance?.iconPath ? (
            <img
              src={planet.appearance.iconPath}
              alt={planet.name}
              className="h-24 w-24 rounded-full object-cover"
            />
          ) : (
            <div className="flex h-24 w-24 items-center justify-center rounded-full bg-gradient-to-br from-blue-500 to-purple-600 text-4xl font-bold text-white">
              {planet.name[0]}
            </div>
          )}
          
          <div className="flex-1">
            <h1 className="text-4xl font-bold text-gray-900 dark:text-white">
              {planet.name}
            </h1>
            <p className="mt-4 text-lg text-gray-600 dark:text-gray-300">
              {planet.lore}
            </p>
          </div>
        </div>
      </div>

      {/* Luas do Planeta */}
      {moons && moons.length > 0 && (
        <div className="mb-8">
          <h2 className="mb-4 text-2xl font-bold text-gray-900 dark:text-white">
            🌙 Luas ({moons.length})
          </h2>
          {moonsLoading ? (
            <LoadingSpinner />
          ) : (
            <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
              {Array.isArray(moons) && moons.map((moon) => (
                <MoonCard
                  key={moon.id}
                  moon={moon}
                  onClick={handleMoonClick}
                />
              ))}
            </div>
          )}
        </div>
      )}

      {/* Missões do Planeta */}
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
              Nenhuma missão disponível neste planeta
            </p>
          </div>
        )}
      </div>
    </div>
  );
}
