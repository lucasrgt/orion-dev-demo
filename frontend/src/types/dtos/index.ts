import type { Appearance, MissionDetail } from "../models";

export interface CreateSolarSystemDto {
  name: string;
}

export interface CreatePlanetDto {
  name: string;
  lore: string;
  appearance: Appearance;
  solarSystemId: string;
}

export interface CreateMoonDto {
  name: string;
  lore: string;
  appearance: Appearance;
  planetId: string;
}

export interface CreateMissionDto {
  name: string;
  detail: MissionDetail;
  slug: string;
  planetId?: string;
  moonId?: string;
  prerequisiteMissionIds: string[];
}

export interface CreateMissionModuleDto {
  name: string;
  description: string;
  order: number;
  missionId: string;
}

export interface CreateExpeditionDto {
  missionId: string;
}

export interface CreateCommentDto {
  content: string;
  missionModuleId: string;
}
