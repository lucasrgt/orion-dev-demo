import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { missionService } from '@/services/missionService';
import { usePlanets } from '@/hooks/usePlanets';
import { createMissionFormSchema, type CreateMissionFormInput } from '@/schemas';
import { AreaCategory, Difficulty } from '@/types';
import toast from 'react-hot-toast';

interface CreateMissionModalProps {
  isOpen: boolean;
  onClose: () => void;
}

export function CreateMissionModal({ isOpen, onClose }: CreateMissionModalProps) {
  const queryClient = useQueryClient();
  const { data: planets } = usePlanets();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreateMissionFormInput>({
    resolver: zodResolver(createMissionFormSchema),
    defaultValues: {
      name: '',
      lore: '',
      planetId: '',
      moonId: '',
      expeditionsNumber: 1,
      explorationTimeInMinutes: 60,
      areaCategory: AreaCategory.FrontEnd,
      difficulty: Difficulty.Medium,
    },
  });

  const { mutate: createMission, isPending } = useMutation({
    mutationFn: missionService.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['missions'] });
      toast.success('Missão criada com sucesso!');
      reset();
      onClose();
    },
    onError: () => {
      toast.error('Erro ao criar missão');
    },
  });

  const onSubmit = (data: CreateMissionFormInput) => {
    createMission(data as any);
  };

  const handleClose = () => {
    reset();
    onClose();
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black/50 p-4">
      <div className="max-h-[90vh] w-full max-w-2xl overflow-y-auto rounded-lg bg-white p-6 shadow-xl dark:bg-slate-800">
        <h2 className="mb-6 text-2xl font-bold text-gray-900 dark:text-white">
          Criar Missão
        </h2>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          {/* Nome */}
          <div>
            <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
              Nome da Missão
            </label>
            <input
              {...register('name')}
              type="text"
              className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
            />
            {errors.name && (
              <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.name.message}</p>
            )}
          </div>

          {/* Planeta */}
          <div>
            <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
              Planeta (Opcional)
            </label>
            <select
              {...register('planetId')}
              className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
            >
              <option value="">Nenhum planeta</option>
              {Array.isArray(planets) && planets.map((planet) => (
                <option key={planet.id} value={planet.id}>
                  {planet.name}
                </option>
              ))}
            </select>
          </div>

          {/* Lore */}
          <div>
            <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
              História/Lore
            </label>
            <textarea
              {...register('lore')}
              rows={4}
              className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
            />
            {errors.lore && (
              <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.lore.message}</p>
            )}
          </div>

          {/* Expedições e Tempo */}
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Número de Expedições
              </label>
              <input
                {...register('expeditionsNumber', { valueAsNumber: true })}
                type="number"
                min="1"
                className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
              />
              {errors.expeditionsNumber && (
                <p className="mt-1 text-sm text-red-600 dark:text-red-400">
                  {errors.expeditionsNumber.message}
                </p>
              )}
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Tempo de Exploração (min)
              </label>
              <input
                {...register('explorationTimeInMinutes', { valueAsNumber: true })}
                type="number"
                min="1"
                className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
              />
              {errors.explorationTimeInMinutes && (
                <p className="mt-1 text-sm text-red-600 dark:text-red-400">
                  {errors.explorationTimeInMinutes.message}
                </p>
              )}
            </div>
          </div>

          {/* Categoria e Dificuldade */}
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Categoria
              </label>
              <select
                {...register('areaCategory')}
                className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
              >
                <option value={AreaCategory.FrontEnd}>Front-End</option>
                <option value={AreaCategory.BackEnd}>Back-End</option>
                <option value={AreaCategory.Mobile}>Mobile</option>
                <option value={AreaCategory.UiDesign}>UI/UX Design</option>
              </select>
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Dificuldade
              </label>
              <select
                {...register('difficulty')}
                className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
              >
                <option value={Difficulty.Lowest}>Muito Fácil</option>
                <option value={Difficulty.Low}>Fácil</option>
                <option value={Difficulty.Medium}>Médio</option>
                <option value={Difficulty.High}>Difícil</option>
                <option value={Difficulty.VeryHigh}>Muito Difícil</option>
              </select>
            </div>
          </div>

          <div className="flex justify-end gap-3 pt-4">
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
