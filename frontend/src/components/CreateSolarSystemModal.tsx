import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { solarSystemService } from '@/services/solarSystemService';
import { createSolarSystemSchema, type CreateSolarSystemInput } from '@/schemas';
import toast from 'react-hot-toast';

interface CreateSolarSystemModalProps {
  isOpen: boolean;
  onClose: () => void;
}

export function CreateSolarSystemModal({ isOpen, onClose }: CreateSolarSystemModalProps) {
  const queryClient = useQueryClient();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateSolarSystemInput>({
    resolver: zodResolver(createSolarSystemSchema),
    defaultValues: {
      name: '',
    },
  });

  const { mutate: createSolarSystem, isPending } = useMutation({
    mutationFn: solarSystemService.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['solarSystems'] });
      toast.success('Sistema Solar criado com sucesso!');
      reset();
      onClose();
    },
    onError: () => {
      toast.error('Erro ao criar Sistema Solar');
    },
  });

  const onSubmit = (data: CreateSolarSystemInput) => {
    createSolarSystem(data);
  };

  const handleClose = () => {
    reset();
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50">
      <div className="w-full max-w-md rounded-lg bg-white p-6 shadow-xl dark:bg-slate-800">
        <h2 className="mb-4 text-2xl font-bold text-gray-900 dark:text-white">
          Criar Sistema Solar
        </h2>

        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="mb-4">
            <label
              htmlFor="name"
              className="block text-sm font-medium text-gray-700 dark:text-gray-300"
            >
              Nome do Sistema Solar
            </label>
            <input
              {...register('name')}
              id="name"
              type="text"
              className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 placeholder-gray-400 focus:border-blue-500 focus:outline-none focus:ring-blue-500 dark:border-gray-600 dark:bg-slate-700 dark:text-white dark:placeholder-gray-500"
              placeholder="Ex: Sistema Solar Alpha"
            />
            {errors.name && (
              <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.name.message}</p>
            )}
          </div>

          <div className="flex justify-end gap-3">
            <button
              type="button"
              onClick={handleClose}
              className="rounded-lg border border-gray-300 px-4 py-2 text-sm font-medium text-gray-700 transition-colors hover:bg-gray-50 dark:border-gray-600 dark:text-gray-300 dark:hover:bg-slate-700"
            >
              Cancelar
            </button>
            <button
              type="submit"
              disabled={isPending}
              className="rounded-lg bg-blue-600 px-4 py-2 text-sm font-medium text-white transition-colors hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-gray-400 dark:bg-blue-500 dark:hover:bg-blue-600"
            >
              {isPending ? 'Criando...' : 'Criar'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
