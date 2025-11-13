import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { planetService } from "@/services/planetService";
import type { CreatePlanetDto } from "@/types";

export function usePlanets() {
  return useQuery({
    queryKey: ["planets"],
    queryFn: planetService.getAll,
  });
}

export function usePlanet(id: string) {
  return useQuery({
    queryKey: ["planet", id],
    queryFn: () => planetService.getById(id),
    enabled: !!id,
  });
}

export function usePlanetsBySolarSystem(solarSystemId: string) {
  return useQuery({
    queryKey: ["planets", "solarSystem", solarSystemId],
    queryFn: () => planetService.getBySolarSystem(solarSystemId),
    enabled: !!solarSystemId,
  });
}

export function useCreatePlanet() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (dto: CreatePlanetDto) => planetService.create(dto),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["planets"] });
    },
  });
}

export function useUpdatePlanet() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: ({ id, dto }: { id: string; dto: CreatePlanetDto }) =>
      planetService.update(id, dto),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["planets"] });
    },
  });
}

export function useDeletePlanet() {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: (id: string) => planetService.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["planets"] });
    },
  });
}
