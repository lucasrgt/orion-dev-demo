import type { Planet } from '@/types';

interface PlanetCardProps {
  planet: Planet;
  onClick: (planet: Planet) => void;
}

export function PlanetCard({ planet, onClick }: PlanetCardProps) {
  return (
    <div
      onClick={() => onClick(planet)}
      className="cursor-pointer rounded-lg border border-gray-200 bg-white p-6 shadow-sm transition-all hover:scale-[1.02] hover:shadow-md dark:border-gray-700 dark:bg-slate-800"
    >
      <div className="mb-4 flex items-center gap-4">
        {planet.appearance?.iconPath ? (
          <img
            src={planet.appearance.iconPath}
            alt={planet.name}
            className="h-16 w-16 rounded-full object-cover"
          />
        ) : (
          <div className="flex h-16 w-16 items-center justify-center rounded-full bg-gradient-to-br from-blue-500 to-purple-600 text-2xl font-bold text-white">
            {planet.name[0]}
          </div>
        )}
        
        <div>
          <h3 className="text-xl font-bold text-gray-900 dark:text-white">
            {planet.name}
          </h3>
          <p className="text-sm text-gray-500 dark:text-gray-400">
            Planeta
          </p>
        </div>
      </div>

      <p className="line-clamp-3 text-sm text-gray-600 dark:text-gray-300">
        {planet.lore}
      </p>

      <div className="mt-4 flex items-center justify-between">
        <span className="text-xs text-gray-500 dark:text-gray-400">
          Sistema Solar ID: {planet.solarSystemId.slice(0, 8)}...
        </span>
      </div>
    </div>
  );
}
