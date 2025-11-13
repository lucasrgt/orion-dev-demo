import { api } from "./api";
import { convertKeysToPascalCase } from "@/utils/caseConverter";
import type { Planet, CreatePlanetDto } from "@/types";

export const planetService = {
  getAll: async (): Promise<Planet[]> => {
    const { data } = await api.get("/planets");
    return data.planets || [];
  },

  getById: async (id: string): Promise<Planet> => {
    const { data } = await api.get(`/planets/${id}`);
    return data;
  },

  getBySolarSystem: async (solarSystemId: string): Promise<Planet[]> => {
    const { data } = await api.get(`/planets/solarsystem/${solarSystemId}`);
    return data;
  },

  create: async (dto: CreatePlanetDto): Promise<Planet> => {
    const { data } = await api.post("/planets", convertKeysToPascalCase(dto));
    return data;
  },

  update: async (id: string, dto: CreatePlanetDto): Promise<Planet> => {
    const { data } = await api.put(
      `/planets/${id}`,
      convertKeysToPascalCase(dto)
    );
    return data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/planets/${id}`);
  },
};
