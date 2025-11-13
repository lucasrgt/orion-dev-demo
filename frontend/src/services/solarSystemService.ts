import { api } from "./api";
import { convertKeysToPascalCase } from "@/utils/caseConverter";
import type { SolarSystem, CreateSolarSystemDto } from "@/types";

export const solarSystemService = {
  getAll: async (): Promise<SolarSystem[]> => {
    const { data } = await api.get("/solarsystems");
    return data.solarSystems || [];
  },

  getById: async (id: string): Promise<SolarSystem> => {
    const { data } = await api.get(`/solarsystems/${id}`);
    return data;
  },

  create: async (dto: CreateSolarSystemDto): Promise<SolarSystem> => {
    const { data } = await api.post(
      "/solarsystems",
      convertKeysToPascalCase(dto)
    );
    return data;
  },

  update: async (
    id: string,
    dto: CreateSolarSystemDto
  ): Promise<SolarSystem> => {
    const { data } = await api.put(
      `/solarsystems/${id}`,
      convertKeysToPascalCase(dto)
    );
    return data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/solarsystems/${id}`);
  },
};
