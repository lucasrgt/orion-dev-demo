import type { AreaCategory, Difficulty } from "../enums";

export interface Gradient {
  startHex: string;
  middleHex: string;
  endHex: string;
}

export interface ColorPalette {
  primaryColorHex: string;
  secondaryColorHex: string;
  gradient: Gradient;
}

export interface Appearance {
  colorPalette: ColorPalette;
  iconPath: string;
  bannerPath: string;
}

export interface MissionDetail {
  expeditionsNumber: number;
  explorationTimeInMinutes: number;
  areaCategory: AreaCategory;
  difficulty: Difficulty;
  lore: string;
}

export interface PrerequisiteMission {
  id: string;
  name: string;
}

export interface SolarSystem {
  id: string;
  name: string;
  createdOn: string;
  updatedOn: string;
}

export interface Planet {
  id: string;
  createdOn: string;
  updatedOn: string;
  name: string;
  lore: string;
  appearance: Appearance;
  solarSystemId: string;
}

export interface Moon {
  id: string;
  createdOn: string;
  updatedOn: string;
  name: string;
  lore: string;
  appearance: Appearance;
  planetId: string;
}

export interface Mission {
  id: string;
  createdOn: string;
  updatedOn: string;
  name: string;
  detail: MissionDetail;
  slug: string;
  planetId?: string;
  moonId?: string;
  prerequisiteMissions: PrerequisiteMission[];
}

export interface MissionModule {
  id: string;
  createdOn: string;
  updatedOn: string;
  name: string;
  description: string;
  order: number;
  missionId: string;
}

export interface Expedition {
  id: string;
  createdOn: string;
  updatedOn: string;
  startedAt: string;
  completedAt?: string;
  status: string;
  missionId: string;
  userId: string;
}

export interface Comment {
  id: string;
  createdOn: string;
  updatedOn: string;
  content: string;
  userId: string;
  missionModuleId: string;
}
