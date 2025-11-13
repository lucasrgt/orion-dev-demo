interface ErrorMessageProps {
  message?: string;
  onRetry?: () => void;
}

export function ErrorMessage({ message, onRetry }: ErrorMessageProps) {
  return (
    <div className="rounded-lg border border-red-200 bg-red-50 p-6 text-center">
      <div className="mb-2 text-4xl">⚠️</div>
      <h3 className="mb-2 text-lg font-semibold text-red-900">
        Erro ao carregar dados
      </h3>
      <p className="mb-4 text-sm text-red-700">
        {message || 'Ocorreu um erro inesperado. Tente novamente.'}
      </p>
      {onRetry && (
        <button
          onClick={onRetry}
          className="rounded-lg bg-red-600 px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-red-700"
        >
          Tentar novamente
        </button>
      )}
    </div>
  );
}
