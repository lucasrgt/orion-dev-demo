import { Link, Outlet } from 'react-router-dom';
import { useAuthStore } from '@/stores';
import { UserMenu } from './UserMenu';

export function Layout() {
  const { isAuthenticated } = useAuthStore();

  return (
    <div className="flex min-h-screen flex-col bg-gray-50 dark:bg-slate-900">
      <nav className="border-b border-gray-200 bg-white shadow-sm dark:border-gray-700 dark:bg-slate-800">
        <div className="container mx-auto flex items-center justify-between px-4 py-4">
          <Link to="/" className="text-2xl font-bold text-blue-600 dark:text-blue-400">
            OrionDev
          </Link>
          
          <div className="flex items-center gap-6">
            <Link
              to="/"
              className="font-medium text-gray-700 transition-colors hover:text-blue-600 dark:text-gray-300 dark:hover:text-blue-400"
            >
              Início
            </Link>
            <Link
              to="/missions"
              className="font-medium text-gray-700 transition-colors hover:text-blue-600 dark:text-gray-300 dark:hover:text-blue-400"
            >
              Missões
            </Link>
            <Link
              to="/planets"
              className="font-medium text-gray-700 transition-colors hover:text-blue-600 dark:text-gray-300 dark:hover:text-blue-400"
            >
              Planetas
            </Link>
            <Link
              to="/solar-systems"
              className="font-medium text-gray-700 transition-colors hover:text-blue-600 dark:text-gray-300 dark:hover:text-blue-400"
            >
              Universo
            </Link>
          </div>

          <div className="flex items-center gap-4">
            {isAuthenticated ? (
              <UserMenu />
            ) : (
              <div className="flex items-center gap-2">
                <Link
                  to="/login"
                  className="rounded-lg px-4 py-2 text-sm font-medium text-gray-700 transition-colors hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-700"
                >
                  Entrar
                </Link>
                <Link
                  to="/register"
                  className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-blue-700 dark:bg-blue-500 dark:hover:bg-blue-600"
                >
                  Criar conta
                </Link>
              </div>
            )}
          </div>
        </div>
      </nav>

      <main className="flex-1 bg-gray-50 dark:bg-slate-900">
        <Outlet />
      </main>

      <footer className="border-t border-gray-200 bg-white py-8 dark:border-gray-700 dark:bg-slate-800">
        <div className="container mx-auto px-4 text-center text-sm text-gray-600 dark:text-gray-400">
          © 2025 OrionDev. Plataforma de aprendizado gamificado.
        </div>
      </footer>
    </div>
  );
}
