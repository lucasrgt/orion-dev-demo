import { api } from "./api";
import { convertKeysToPascalCase } from "@/utils/caseConverter";
import type { Moon, CreateMoonDto } from "@/types";

export const moonService = {
  getAll: async (): Promise<Moon[]> => {
    const { data } = await api.get("/moons");
    return data;
  },

  getById: async (id: string): Promise<Moon> => {
    const { data } = await api.get(`/moons/${id}`);
    return data;
  },

  getByPlanet: async (planetId: string): Promise<Moon[]> => {
    const { data } = await api.get(`/moons/planet/${planetId}`);
    return data;
  },

  create: async (dto: CreateMoonDto): Promise<Moon> => {
    const { data } = await api.post("/moons", convertKeysToPascalCase(dto));
    return data;
  },

  update: async (id: string, dto: CreateMoonDto): Promise<Moon> => {
    const { data } = await api.put(
      `/moons/${id}`,
      convertKeysToPascalCase(dto)
    );
    return data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/moons/${id}`);
  },
};
