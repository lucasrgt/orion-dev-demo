export function HomePage() {
  return (
    <div className="container mx-auto px-4 py-16">
      <div className="mx-auto max-w-4xl text-center">
        <h1 className="mb-6 text-6xl font-bold text-gray-900 dark:text-white">
          OrionDev
        </h1>
        
        <p className="mb-8 text-xl text-gray-600 dark:text-gray-300">
          Plataforma de aprendizado gamificado para desenvolvedores
        </p>
        <p className="mb-12 text-lg text-gray-500 dark:text-gray-400">
          Explore sistemas solares, planetas e luas enquanto aprende 
          desenvolvimento de software através de missões práticas.
        </p>
        
        <div className="mt-16 grid gap-8 md:grid-cols-3">
          <div className="rounded-lg border border-gray-200 bg-white p-6 dark:border-gray-700 dark:bg-slate-800">
            <h3 className="mb-2 text-lg font-bold text-gray-900 dark:text-white">
              Missões Práticas
            </h3>
            <p className="text-sm text-gray-600 dark:text-gray-300">
              Aprenda através de desafios reais e projetos práticos
            </p>
          </div>

          <div className="rounded-lg border border-gray-200 bg-white p-6 dark:border-gray-700 dark:bg-slate-800">
            <h3 className="mb-2 text-lg font-bold text-gray-900 dark:text-white">
              Universo Gamificado
            </h3>
            <p className="text-sm text-gray-600 dark:text-gray-300">
              Explore sistemas solares e desbloqueie novos conteúdos
            </p>
          </div>

          <div className="rounded-lg border border-gray-200 bg-white p-6 dark:border-gray-700 dark:bg-slate-800">
            <h3 className="mb-2 text-lg font-bold text-gray-900 dark:text-white">
              Módulos Estruturados
            </h3>
            <p className="text-sm text-gray-600 dark:text-gray-300">
              Conteúdo organizado e progressivo para seu aprendizado
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}
