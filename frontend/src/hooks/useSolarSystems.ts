import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { solarSystemService } from "@/services/solarSystemService";
import type { CreateSolarSystemDto } from "@/types";

export function useSolarSystems() {
  const {
    data: solarSystems,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["solarSystems"],
    queryFn: solarSystemService.getAll,
  });

  return {
    solarSystems,
    isLoading,
    error,
  };
}

export function useSolarSystem(id: string) {
  const {
    data: solarSystem,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["solarSystem", id],
    queryFn: () => solarSystemService.getById(id),
    enabled: !!id,
  });

  return {
    solarSystem,
    isLoading,
    error,
  };
}

export function useCreateSolarSystem() {
  const queryClient = useQueryClient();

  const {
    mutate: createSolarSystem,
    isPending,
    error,
  } = useMutation({
    mutationFn: (dto: CreateSolarSystemDto) => solarSystemService.create(dto),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["solarSystems"] });
    },
  });

  return {
    createSolarSystem,
    isCreating: isPending,
    error,
  };
}

export function useUpdateSolarSystem() {
  const queryClient = useQueryClient();

  const {
    mutate: updateSolarSystem,
    isPending,
    error,
  } = useMutation({
    mutationFn: ({ id, dto }: { id: string; dto: CreateSolarSystemDto }) =>
      solarSystemService.update(id, dto),
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: ["solarSystems"] });
      queryClient.invalidateQueries({
        queryKey: ["solarSystem", variables.id],
      });
    },
  });

  return {
    updateSolarSystem,
    isUpdating: isPending,
    error,
  };
}

export function useDeleteSolarSystem() {
  const queryClient = useQueryClient();

  const {
    mutate: deleteSolarSystem,
    isPending,
    error,
  } = useMutation({
    mutationFn: (id: string) => solarSystemService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["solarSystems"] });
    },
  });

  return {
    deleteSolarSystem,
    isDeleting: isPending,
    error,
  };
}
