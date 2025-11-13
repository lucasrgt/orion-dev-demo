import { api } from "./api";
import { convertKeysToPascalCase } from "@/utils/caseConverter";
import type { Mission, CreateMissionDto } from "@/types";

export const missionService = {
  getAll: async (): Promise<Mission[]> => {
    const { data } = await api.get("/missions");
    return data.mission || [];
  },

  getById: async (id: string): Promise<Mission> => {
    const { data } = await api.get(`/missions/${id}`);
    return data;
  },

  getBySlug: async (slug: string): Promise<Mission> => {
    const { data } = await api.get(`/missions/slug/${slug}`);
    return data;
  },

  getByPlanet: async (planetId: string): Promise<Mission[]> => {
    const { data } = await api.get(`/missions/planet/${planetId}`);
    return data;
  },

  getByMoon: async (moonId: string): Promise<Mission[]> => {
    const { data } = await api.get(`/missions/moon/${moonId}`);
    return data;
  },

  create: async (dto: CreateMissionDto): Promise<Mission> => {
    const { data } = await api.post("/missions", convertKeysToPascalCase(dto));
    return data;
  },

  update: async (id: string, dto: CreateMissionDto): Promise<Mission> => {
    const { data } = await api.put(
      `/missions/${id}`,
      convertKeysToPascalCase(dto)
    );
    return data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/missions/${id}`);
  },
};
