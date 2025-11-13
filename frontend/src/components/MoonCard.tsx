import type { Moon } from '@/types';

interface MoonCardProps {
  moon: Moon;
  onClick: (moon: Moon) => void;
}

export function MoonCard({ moon, onClick }: MoonCardProps) {
  return (
    <div
      onClick={() => onClick(moon)}
      className="cursor-pointer rounded-lg border border-gray-200 bg-white p-6 shadow-sm transition-all hover:scale-[1.02] hover:shadow-md dark:border-gray-700 dark:bg-slate-800"
    >
      <div className="mb-4 flex items-center gap-4">
        {moon.appearance?.iconPath ? (
          <img
            src={moon.appearance.iconPath}
            alt={moon.name}
            className="h-12 w-12 rounded-full object-cover"
          />
        ) : (
          <div className="flex h-12 w-12 items-center justify-center rounded-full bg-gradient-to-br from-gray-400 to-gray-600 text-lg font-bold text-white">
            {moon.name[0]}
          </div>
        )}
        
        <div>
          <h3 className="text-lg font-bold text-gray-900 dark:text-white">
            {moon.name}
          </h3>
          <p className="text-xs text-gray-500 dark:text-gray-400">
            Lua
          </p>
        </div>
      </div>

      <p className="line-clamp-2 text-sm text-gray-600 dark:text-gray-300">
        {moon.lore}
      </p>

      <div className="mt-4 flex items-center justify-between">
        <span className="text-xs text-gray-500 dark:text-gray-400">
          Planeta ID: {moon.planetId.slice(0, 8)}...
        </span>
      </div>
    </div>
  );
}
