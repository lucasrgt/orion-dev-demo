import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { planetService } from '@/services/planetService';
import { useSolarSystems } from '@/hooks/useSolarSystems';
import { createPlanetFormSchema, type CreatePlanetFormInput } from '@/schemas';
import type { CreatePlanetDto } from '@/types';
import toast from 'react-hot-toast';

interface CreatePlanetModalProps {
  isOpen: boolean;
  onClose: () => void;
}

export function CreatePlanetModal({ isOpen, onClose }: CreatePlanetModalProps) {
  const queryClient = useQueryClient();
  const { solarSystems } = useSolarSystems();

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<CreatePlanetFormInput>({
    resolver: zodResolver(createPlanetFormSchema),
    defaultValues: {
      name: '',
      lore: '',
      solarSystemId: '',
      iconPath: '',
      bannerPath: '',
      primaryColorHex: '#3B82F6',
      secondaryColorHex: '#1E40AF',
      gradientStart: '#60A5FA',
      gradientMiddle: '#3B82F6',
      gradientEnd: '#1E3A8A',
    },
  });

  const { mutate: createPlanet, isPending } = useMutation({
    mutationFn: planetService.create,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['planets'] });
      toast.success('Planeta criado com sucesso!');
      reset();
      onClose();
    },
    onError: () => {
      toast.error('Erro ao criar planeta');
    },
  });

  const onSubmit = (data: CreatePlanetFormInput) => {
    const dto: CreatePlanetDto = {
      name: data.name,
      lore: data.lore,
      solarSystemId: data.solarSystemId,
      appearance: {
        iconPath: data.iconPath,
        bannerPath: data.bannerPath,
        colorPalette: {
          primaryColorHex: data.primaryColorHex,
          secondaryColorHex: data.secondaryColorHex,
          gradient: {
            startHex: data.gradientStart,
            middleHex: data.gradientMiddle,
            endHex: data.gradientEnd,
          },
        },
      },
    };

    createPlanet(dto);
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
          Criar Planeta
        </h2>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          {/* Nome */}
          <div>
            <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
              Nome
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

          {/* Sistema Solar */}
          <div>
            <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
              Sistema Solar
            </label>
            <select
              {...register('solarSystemId')}
              className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
            >
              <option value="">Selecione um sistema solar</option>
              {Array.isArray(solarSystems) && solarSystems.map((system) => (
                <option key={system.id} value={system.id}>
                  {system.name}
                </option>
              ))}
            </select>
            {errors.solarSystemId && (
              <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.solarSystemId.message}</p>
            )}
          </div>

          {/* Lore */}
          <div>
            <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
              Descrição/Lore
            </label>
            <textarea
              {...register('lore')}
              rows={3}
              className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
            />
            {errors.lore && (
              <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.lore.message}</p>
            )}
          </div>

          {/* Aparência */}
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Icon Path
              </label>
              <input
                {...register('iconPath')}
                type="text"
                placeholder="/icons/planet.svg"
                className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
              />
              {errors.iconPath && (
                <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.iconPath.message}</p>
              )}
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Banner Path
              </label>
              <input
                {...register('bannerPath')}
                type="text"
                placeholder="/banners/planet.jpg"
                className="mt-1 block w-full rounded-lg border border-gray-300 bg-white px-3 py-2 text-gray-900 dark:border-gray-600 dark:bg-slate-700 dark:text-white"
              />
              {errors.bannerPath && (
                <p className="mt-1 text-sm text-red-600 dark:text-red-400">{errors.bannerPath.message}</p>
              )}
            </div>
          </div>

          {/* Cores */}
          <div className="grid grid-cols-2 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Cor Primária
              </label>
              <input
                {...register('primaryColorHex')}
                type="color"
                className="mt-1 block h-10 w-full rounded-lg border border-gray-300 dark:border-gray-600"
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Cor Secundária
              </label>
              <input
                {...register('secondaryColorHex')}
                type="color"
                className="mt-1 block h-10 w-full rounded-lg border border-gray-300 dark:border-gray-600"
              />
            </div>
          </div>

          {/* Gradiente */}
          <div className="grid grid-cols-3 gap-4">
            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Gradiente Início
              </label>
              <input
                {...register('gradientStart')}
                type="color"
                className="mt-1 block h-10 w-full rounded-lg border border-gray-300 dark:border-gray-600"
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Gradiente Meio
              </label>
              <input
                {...register('gradientMiddle')}
                type="color"
                className="mt-1 block h-10 w-full rounded-lg border border-gray-300 dark:border-gray-600"
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700 dark:text-gray-300">
                Gradiente Fim
              </label>
              <input
                {...register('gradientEnd')}
                type="color"
                className="mt-1 block h-10 w-full rounded-lg border border-gray-300 dark:border-gray-600"
              />
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
