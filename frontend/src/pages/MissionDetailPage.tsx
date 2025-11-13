import { useParams } from 'react-router-dom';
import { useMissionBySlug } from '@/hooks/useMissions';
import { useCreateExpedition } from '@/hooks/useExpeditions';
import { LoadingSpinner } from '@/components/LoadingSpinner';
import { ErrorMessage } from '@/components/ErrorMessage';

export function MissionDetailPage() {
  const { slug } = useParams<{ slug: string }>();
  const { mission, isLoading, error } = useMissionBySlug(slug);
  const { handleStartExpedition, isCreating } = useCreateExpedition();

  if (isLoading) {
    return (
      <div className="container mx-auto px-4 py-8">
        <LoadingSpinner />
      </div>
    );
  }

  if (error || !mission) {
    return (
      <div className="container mx-auto px-4 py-8">
        <ErrorMessage message="Missão não encontrada" />
      </div>
    );
  }

  const difficultyColors = {
    Lowest: 'bg-green-100 text-green-800',
    Low: 'bg-green-200 text-green-900',
    Medium: 'bg-yellow-100 text-yellow-800',
    High: 'bg-orange-100 text-orange-800',
    VeryHigh: 'bg-red-100 text-red-800',
  };

  const categoryColors = {
    FrontEnd: 'bg-blue-100 text-blue-800',
    BackEnd: 'bg-purple-100 text-purple-800',
    Mobile: 'bg-pink-100 text-pink-800',
    UiDesign: 'bg-indigo-100 text-indigo-800',
  };

  return (
    <div className="container mx-auto px-4 py-8">
      <div className="mb-8">
        <h1 className="text-4xl font-bold text-gray-900">{mission.name}</h1>
        <div className="mt-4 flex flex-wrap items-center gap-3">
          <span
            className={`rounded-full px-4 py-2 text-sm font-semibold ${
              difficultyColors[mission.detail.difficulty]
            }`}
          >
            {mission.detail.difficulty}
          </span>
          <span
            className={`rounded-full px-4 py-2 text-sm font-medium ${
              categoryColors[mission.detail.areaCategory]
            }`}
          >
            {mission.detail.areaCategory}
          </span>
          <span className="text-sm text-gray-600">
            ⏱️ {mission.detail.explorationTimeInMinutes} minutos
          </span>
          <span className="text-sm text-gray-600">
            🚀 {mission.detail.expeditionsNumber} expedições
          </span>
        </div>
      </div>

      <div className="grid gap-8 lg:grid-cols-3">
        <div className="lg:col-span-2">
          <div className="rounded-lg border border-gray-200 bg-white p-6 shadow-sm dark:border-gray-700 dark:bg-slate-800">
            <h2 className="mb-4 text-2xl font-bold text-gray-900 dark:text-white">Sobre</h2>
            <p className="whitespace-pre-line text-gray-700 dark:text-gray-300">
              {mission.detail.lore}
            </p>
          </div>

          {mission.prerequisiteMissions.length > 0 && (
            <div className="mt-6 rounded-lg border border-gray-200 bg-white p-6 shadow-sm dark:border-gray-700 dark:bg-slate-800">
              <h2 className="mb-4 text-xl font-bold text-gray-900 dark:text-white">
                Pré-requisitos
              </h2>
              <div className="flex flex-wrap gap-3">
                {Array.isArray(mission.prerequisiteMissions) && mission.prerequisiteMissions.map((prereq) => (
                  <div
                    key={prereq.id}
                    className="rounded-lg border border-gray-200 bg-gray-50 px-4 py-2 dark:border-gray-600 dark:bg-slate-700"
                  >
                    <p className="text-sm font-medium text-gray-900 dark:text-white">
                      {prereq.name}
                    </p>
                  </div>
                ))}
              </div>
            </div>
          )}
        </div>

        <div>
          <div className="sticky top-8 rounded-lg border border-gray-200 bg-white p-6 shadow-sm dark:border-gray-700 dark:bg-slate-800">
            <h3 className="mb-4 text-xl font-bold text-gray-900 dark:text-white">
              Iniciar Expedição
            </h3>
            <button
              onClick={() => handleStartExpedition(mission.id)}
              disabled={isCreating}
              className="w-full rounded-lg bg-blue-600 px-6 py-3 font-semibold text-white transition-colors hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-gray-400 dark:bg-blue-500 dark:hover:bg-blue-600"
            >
              {isCreating ? 'Iniciando...' : 'Começar Missão'}
            </button>
            <p className="mt-4 text-xs text-gray-500 dark:text-gray-400">
              Ao iniciar, você terá acesso aos módulos e desafios desta missão.
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}
