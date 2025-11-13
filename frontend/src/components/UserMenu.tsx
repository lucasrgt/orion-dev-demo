import { useAuthStore } from '@/stores';
import { useLogout } from '@/hooks/useAuth';

export function UserMenu() {
  const { user, isAuthenticated } = useAuthStore();
  const { logout } = useLogout();

  if (!isAuthenticated || !user) {
    return null;
  }

  return (
    <div className="relative">
      <div className="flex items-center gap-3">
        <div className="flex h-10 w-10 items-center justify-center rounded-full bg-blue-600 text-sm font-semibold text-white dark:bg-blue-500">
          {user.name.charAt(0).toUpperCase()}
        </div>
        <div className="hidden md:block">
          <p className="text-sm font-medium text-gray-900 dark:text-white">{user.name}</p>
          <p className="text-xs text-gray-500 dark:text-gray-400">{user.email}</p>
        </div>
        <button
          onClick={() => logout()}
          className="ml-2 cursor-pointer rounded-lg px-3 py-2 text-sm font-medium text-gray-700 transition-colors hover:bg-gray-100 dark:text-gray-300 dark:hover:bg-gray-800"
        >
          Sair
        </button>
      </div>
    </div>
  );
}
