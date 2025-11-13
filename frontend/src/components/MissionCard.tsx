import type { Mission } from '@/types';

interface MissionCardProps {
  mission: Mission;
  onSelect: (mission: Mission) => void;
}

export function MissionCard({ mission, onSelect }: MissionCardProps) {
  const difficultyColors = {
    Lowest: 'bg-green-100 text-green-800 dark:bg-green-900 dark:text-green-200',
    Low: 'bg-green-200 text-green-900 dark:bg-green-800 dark:text-green-100',
    Medium: 'bg-yellow-100 text-yellow-800 dark:bg-yellow-900 dark:text-yellow-200',
    High: 'bg-orange-100 text-orange-800 dark:bg-orange-900 dark:text-orange-200',
    VeryHigh: 'bg-red-100 text-red-800 dark:bg-red-900 dark:text-red-200',
  };

  const categoryColors = {
    FrontEnd: 'bg-blue-100 text-blue-800 dark:bg-blue-900 dark:text-blue-200',
    BackEnd: 'bg-purple-100 text-purple-800 dark:bg-purple-900 dark:text-purple-200',
    Mobile: 'bg-pink-100 text-pink-800 dark:bg-pink-900 dark:text-pink-200',
    UiDesign: 'bg-indigo-100 text-indigo-800 dark:bg-indigo-900 dark:text-indigo-200',
  };

  return (
    <div
      onClick={() => onSelect(mission)}
      className="cursor-pointer rounded-lg border border-gray-200 bg-white p-6 shadow-sm transition-all hover:scale-[1.02] hover:shadow-md dark:border-gray-700 dark:bg-slate-800"
    >
      <div className="mb-4 flex items-start justify-between">
        <h3 className="text-xl font-bold text-gray-900 dark:text-white">{mission.name}</h3>
        <span
          className={`rounded-full px-3 py-1 text-xs font-semibold ${
            difficultyColors[mission.detail.difficulty]
          }`}
        >
          {mission.detail.difficulty}
        </span>
      </div>

      <p className="mb-4 line-clamp-3 text-sm text-gray-600 dark:text-gray-300">
        {mission.detail.lore}
      </p>

      <div className="flex items-center gap-2">
        <span
          className={`rounded-full px-3 py-1 text-xs font-medium ${
            categoryColors[mission.detail.areaCategory]
          }`}
        >
          {mission.detail.areaCategory}
        </span>
        <span className="text-xs text-gray-500">
          {mission.detail.explorationTimeInMinutes} min
        </span>
        <span className="text-xs text-gray-500">
          {mission.detail.expeditionsNumber} expedições
        </span>
      </div>

      {mission.prerequisiteMissions.length > 0 && (
        <div className="mt-4 border-t pt-4">
          <p className="text-xs font-medium text-gray-700">Pré-requisitos:</p>
          <div className="mt-2 flex flex-wrap gap-2">
            {Array.isArray(mission.prerequisiteMissions) && mission.prerequisiteMissions.map((prereq) => (
              <span
                key={prereq.id}
                className="rounded-full bg-gray-100 px-2 py-1 text-xs text-gray-600"
              >
                {prereq.name}
              </span>
            ))}
          </div>
        </div>
      )}
    </div>
  );
}
