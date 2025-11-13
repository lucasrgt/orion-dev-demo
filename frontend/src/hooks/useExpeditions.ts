import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { expeditionService } from "@/services/expeditionService";
import type { CreateExpeditionDto } from "@/types";

export function useExpeditions() {
  const {
    data: expeditions,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["expeditions"],
    queryFn: expeditionService.getAll,
  });

  return {
    expeditions,
    isLoading,
    error,
  };
}

export function useExpedition(id?: string) {
  const {
    data: expedition,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["expedition", id],
    queryFn: () => expeditionService.getById(id!),
    enabled: !!id,
  });

  return {
    expedition,
    isLoading,
    error,
  };
}

export function useUserExpeditions(userId?: string) {
  const {
    data: expeditions,
    isLoading,
    error,
  } = useQuery({
    queryKey: ["expeditions", "user", userId],
    queryFn: () => expeditionService.getByUser(userId!),
    enabled: !!userId,
  });

  return {
    expeditions,
    isLoading,
    error,
  };
}

export function useCreateExpedition() {
  const queryClient = useQueryClient();

  const {
    mutate: createExpedition,
    isPending,
    error,
  } = useMutation({
    mutationFn: (dto: CreateExpeditionDto) => expeditionService.create(dto),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["expeditions"] });
    },
  });

  const handleStartExpedition = (missionId: string) => {
    createExpedition({ missionId });
  };

  return {
    createExpedition,
    handleStartExpedition,
    isCreating: isPending,
    error,
  };
}

export function useCompleteExpedition() {
  const queryClient = useQueryClient();

  const {
    mutate: completeExpedition,
    isPending,
    error,
  } = useMutation({
    mutationFn: (id: string) => expeditionService.complete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["expeditions"] });
    },
  });

  const handleCompleteExpedition = (id: string) => {
    completeExpedition(id);
  };

  return {
    completeExpedition,
    handleCompleteExpedition,
    isCompleting: isPending,
    error,
  };
}
