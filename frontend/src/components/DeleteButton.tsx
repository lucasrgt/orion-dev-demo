import { useState } from 'react';

interface DeleteButtonProps {
  onConfirm: () => void;
  isDeleting?: boolean;
  itemName: string;
}

export function DeleteButton({ onConfirm, isDeleting, itemName }: DeleteButtonProps) {
  const [showConfirm, setShowConfirm] = useState(false);

  const handleDelete = () => {
    onConfirm();
    setShowConfirm(false);
  };

  if (showConfirm) {
    return (
      <div className="flex items-center gap-2">
        <span className="text-sm text-gray-600 dark:text-gray-300">
          Confirmar exclusão de "{itemName}"?
        </span>
        <button
          onClick={handleDelete}
          disabled={isDeleting}
          className="rounded bg-red-600 px-3 py-1 text-sm font-medium text-white transition-colors hover:bg-red-700 disabled:bg-gray-400"
        >
          {isDeleting ? 'Excluindo...' : 'Sim'}
        </button>
        <button
          onClick={() => setShowConfirm(false)}
          className="rounded bg-gray-300 px-3 py-1 text-sm font-medium text-gray-700 transition-colors hover:bg-gray-400 dark:bg-gray-600 dark:text-gray-200"
        >
          Não
        </button>
      </div>
    );
  }

  return (
    <button
      onClick={() => setShowConfirm(true)}
      className="rounded-lg bg-red-600 px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-red-700 dark:bg-red-500 dark:hover:bg-red-600"
    >
      🗑️ Excluir
    </button>
  );
}
