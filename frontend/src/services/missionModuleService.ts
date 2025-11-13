import { api } from "./api";
import { convertKeysToPascalCase } from "@/utils/caseConverter";
import type { MissionModule, CreateMissionModuleDto } from "@/types";

export const missionModuleService = {
  getAll: async (): Promise<MissionModule[]> => {
    const { data } = await api.get("/missionmodules");
    return data;
  },

  getById: async (id: string): Promise<MissionModule> => {
    const { data } = await api.get(`/missionmodules/${id}`);
    return data;
  },

  getByMission: async (missionId: string): Promise<MissionModule[]> => {
    const { data } = await api.get(`/missionmodules/mission/${missionId}`);
    return data;
  },

  create: async (dto: CreateMissionModuleDto): Promise<MissionModule> => {
    const { data } = await api.post(
      "/missionmodules",
      convertKeysToPascalCase(dto)
    );
    return data;
  },

  update: async (
    id: string,
    dto: CreateMissionModuleDto
  ): Promise<MissionModule> => {
    const { data } = await api.put(
      `/missionmodules/${id}`,
      convertKeysToPascalCase(dto)
    );
    return data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/missionmodules/${id}`);
  },
};
