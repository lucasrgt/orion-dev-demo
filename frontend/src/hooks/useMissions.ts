import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { missionService } from "@/services/missionService";
import type { CreateMissionDto } from "@/types";

interface UseMissionsFilters {
  planetId?: string;
  moonId?: string;
}

export function useMissions(filters?: UseMissionsFilters) {
  const {
    data: missions,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["missions", filters],
    queryFn: async () => {
      if (filters?.planetId) {
        return missionService.getByPlanet(filters.planetId);
      }
      if (filters?.moonId) {
        return missionService.getByMoon(filters.moonId);
      }
      return missionService.getAll();
    },
  });

  return {
    missions,
    isLoading,
    error,
  };
}

export function useMission(id?: string) {
  const {
    data: mission,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["mission", id],
    queryFn: () => missionService.getById(id!),
    enabled: !!id,
  });

  return {
    mission,
    isLoading,
    error,
  };
}

export function useMissionBySlug(slug?: string) {
  const {
    data: mission,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["mission", "slug", slug],
    queryFn: () => missionService.getBySlug(slug!),
    enabled: !!slug,
  });

  return {
    mission,
    isLoading,
    error,
  };
}

export function useMissionsByPlanet(planetId?: string) {
  const {
    data: missions,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["missions", "planet", planetId],
    queryFn: () => missionService.getByPlanet(planetId!),
    enabled: !!planetId,
  });

  return {
    missions,
    isLoading,
    error,
  };
}

export function useCreateMission() {
  const queryClient = useQueryClient();

  const {
    mutate: createMission,
    isPending,
    error,
  } = useMutation({
    mutationFn: (dto: CreateMissionDto) => missionService.create(dto),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["missions"] });
    },
  });

  return {
    createMission,
    isCreating: isPending,
    error,
  };
}

export function useUpdateMission() {
  const queryClient = useQueryClient();

  const {
    mutate: updateMission,
    isPending,
    error,
  } = useMutation({
    mutationFn: ({ id, dto }: { id: string; dto: CreateMissionDto }) =>
      missionService.update(id, dto),
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: ["missions"] });
      queryClient.invalidateQueries({ queryKey: ["mission", variables.id] });
    },
  });

  return {
    updateMission,
    isUpdating: isPending,
    error,
  };
}

export function useDeleteMission() {
  const queryClient = useQueryClient();

  const {
    mutate: deleteMission,
    isPending,
    error,
  } = useMutation({
    mutationFn: (id: string) => missionService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["missions"] });
    },
  });

  return {
    deleteMission,
    isDeleting: isPending,
    error,
  };
}
