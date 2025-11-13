import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { moonService } from "@/services/moonService";
import type { CreateMoonDto } from "@/types";

export function useMoons() {
  return useQuery({
    queryKey: ["moons"],
    queryFn: moonService.getAll,
  });
}

export function useMoon(id: string) {
  return useQuery({
    queryKey: ["moon", id],
    queryFn: () => moonService.getById(id),
    enabled: !!id,
  });
}

export function useMoonsByPlanet(planetId: string) {
  return useQuery({
    queryKey: ["moons", "planet", planetId],
    queryFn: () => moonService.getByPlanet(planetId),
    enabled: !!planetId,
  });
}

export function useCreateMoon() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (dto: CreateMoonDto) => moonService.create(dto),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["moons"] });
    },
  });
}

export function useUpdateMoon() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, dto }: { id: string; dto: CreateMoonDto }) =>
      moonService.update(id, dto),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["moons"] });
    },
  });
}

export function useDeleteMoon() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: string) => moonService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["moons"] });
    },
  });
}
