import { z } from "zod";
import { Difficulty, AreaCategory } from "@/types/enums";

export const difficultySchema = z.enum(Difficulty);
export const areaCategorySchema = z.enum(AreaCategory);

export const missionDetailSchema = z.object({
  expeditionsNumber: z.number().int().positive(),
  explorationTimeInMinutes: z.number().int().positive(),
  areaCategory: areaCategorySchema,
  difficulty: difficultySchema,
  lore: z.string().min(10).max(1000),
});

export const createMissionSchema = z.object({
  name: z.string().min(3).max(100),
  detail: missionDetailSchema,
  slug: z
    .string()
    .min(3)
    .max(100)
    .regex(/^[a-z0-9-]+$/),
  planetId: z.uuid().optional(),
  moonId: z.uuid().optional(),
  prerequisiteMissionIds: z.array(z.uuid()).default([]),
});

export const gradientSchema = z.object({
  startHex: z.string().regex(/^#[0-9A-Fa-f]{6}$/),
  middleHex: z.string().regex(/^#[0-9A-Fa-f]{6}$/),
  endHex: z.string().regex(/^#[0-9A-Fa-f]{6}$/),
});

export const colorPaletteSchema = z.object({
  primaryColorHex: z.string().regex(/^#[0-9A-Fa-f]{6}$/),
  secondaryColorHex: z.string().regex(/^#[0-9A-Fa-f]{6}$/),
  gradient: gradientSchema,
});

export const appearanceSchema = z.object({
  colorPalette: colorPaletteSchema,
  iconPath: z.string().min(1, "Icon path é obrigatório"),
  bannerPath: z.string().min(1, "Banner path é obrigatório"),
});

export const createPlanetSchema = z.object({
  name: z.string().min(3, "Nome deve ter no mínimo 3 caracteres").max(100),
  lore: z
    .string()
    .min(10, "Descrição deve ter no mínimo 10 caracteres")
    .max(1000),
  appearance: appearanceSchema,
  solarSystemId: z.string().min(1, "Selecione um sistema solar"),
});

// Simplified form schema for planet creation (flattened)
export const createPlanetFormSchema = z.object({
  name: z.string().min(3, "Nome deve ter no mínimo 3 caracteres").max(100),
  lore: z
    .string()
    .min(10, "Descrição deve ter no mínimo 10 caracteres")
    .max(1000),
  solarSystemId: z.string().min(1, "Selecione um sistema solar"),
  iconPath: z.string().min(1, "Icon path é obrigatório"),
  bannerPath: z.string().min(1, "Banner path é obrigatório"),
  primaryColorHex: z.string().regex(/^#[0-9A-Fa-f]{6}$/, "Cor inválida"),
  secondaryColorHex: z.string().regex(/^#[0-9A-Fa-f]{6}$/, "Cor inválida"),
  gradientStart: z.string().regex(/^#[0-9A-Fa-f]{6}$/, "Cor inválida"),
  gradientMiddle: z.string().regex(/^#[0-9A-Fa-f]{6}$/, "Cor inválida"),
  gradientEnd: z.string().regex(/^#[0-9A-Fa-f]{6}$/, "Cor inválida"),
});

export const createSolarSystemSchema = z.object({
  name: z.string().min(3).max(100),
});

// Simplified form schema for mission creation (flattened)
export const createMissionFormSchema = z.object({
  name: z.string().min(3, "Nome deve ter no mínimo 3 caracteres").max(100),
  lore: z
    .string()
    .min(10, "História deve ter no mínimo 10 caracteres")
    .max(1000),
  planetId: z.string().optional(),
  moonId: z.string().optional(),
  expeditionsNumber: z.number().int().positive("Deve ser maior que 0"),
  explorationTimeInMinutes: z.number().int().positive("Deve ser maior que 0"),
  areaCategory: areaCategorySchema,
  difficulty: difficultySchema,
});

export const createExpeditionSchema = z.object({
  missionId: z.string().uuid(),
});

export const createMissionModuleSchema = z.object({
  name: z.string().min(3).max(100),
  description: z.string().min(10).max(500),
  order: z.number().int().nonnegative(),
  missionId: z.uuid(),
});

export const createCommentSchema = z.object({
  content: z.string().min(1).max(1000),
  missionModuleId: z.uuid(),
});

export const loginSchema = z.object({
  email: z.email(),
  password: z.string().min(6),
});

export const registerSchema = z
  .object({
    name: z.string().min(3).max(100),
    email: z.email(),
    password: z.string().min(6).max(100),
    confirmPassword: z.string(),
  })
  .refine((data) => data.password === data.confirmPassword, {
    message: "Passwords don't match",
    path: ["confirmPassword"],
  });

export type CreateMissionInput = z.infer<typeof createMissionSchema>;
export type CreateMissionFormInput = z.infer<typeof createMissionFormSchema>;
export type CreatePlanetInput = z.infer<typeof createPlanetSchema>;
export type CreatePlanetFormInput = z.infer<typeof createPlanetFormSchema>;
export type CreateSolarSystemInput = z.infer<typeof createSolarSystemSchema>;
export type CreateExpeditionInput = z.infer<typeof createExpeditionSchema>;
export type CreateMissionModuleInput = z.infer<
  typeof createMissionModuleSchema
>;
export type CreateCommentInput = z.infer<typeof createCommentSchema>;
export type LoginInput = z.infer<typeof loginSchema>;
export type RegisterInput = z.infer<typeof registerSchema>;
