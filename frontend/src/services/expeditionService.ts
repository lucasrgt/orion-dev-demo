import { api } from "./api";
import { convertKeysToPascalCase } from "@/utils/caseConverter";
import type { Expedition, CreateExpeditionDto } from "@/types";

export const expeditionService = {
  getAll: async (): Promise<Expedition[]> => {
    const { data } = await api.get("/expeditions");
    return data;
  },

  getById: async (id: string): Promise<Expedition> => {
    const { data } = await api.get(`/expeditions/${id}`);
    return data;
  },

  getByUser: async (userId: string): Promise<Expedition[]> => {
    const { data } = await api.get(`/expeditions/user/${userId}`);
    return data;
  },

  create: async (dto: CreateExpeditionDto): Promise<Expedition> => {
    const { data } = await api.post(
      "/expeditions",
      convertKeysToPascalCase(dto)
    );
    return data;
  },

  complete: async (id: string): Promise<Expedition> => {
    const { data } = await api.patch(`/expeditions/${id}/complete`);
    return data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/expeditions/${id}`);
  },
};
