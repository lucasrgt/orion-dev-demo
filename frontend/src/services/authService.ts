import { api } from "./api";
import { convertKeysToPascalCase } from "@/utils/caseConverter";

interface LoginCredentials {
  email: string;
  password: string;
}

interface RegisterData {
  name: string;
  email: string;
  password: string;
}

interface AuthResponse {
  user: {
    id: string;
    name: string;
    email: string;
    role: string;
  };
  token: string;
}

export const authService = {
  login: async (credentials: LoginCredentials): Promise<AuthResponse> => {
    const token = await api.post(
      "/auth/login",
      convertKeysToPascalCase(credentials)
    );

    return {
      token: token.data,
      user: {
        id: "",
        name: credentials.email.split("@")[0],
        email: credentials.email,
        role: "User",
      },
    };
  },

  register: async (userData: RegisterData): Promise<AuthResponse> => {
    const registerDto = {
      ...userData,
      fullName: userData.name,
      nickName: userData.name,
    };
    delete (registerDto as any).name;

    const token = await api.post(
      "/auth/register",
      convertKeysToPascalCase(registerDto)
    );

    return {
      token: token.data,
      user: {
        id: "",
        name: userData.name,
        email: userData.email,
        role: "User",
      },
    };
  },

  logout: async (): Promise<void> => {
    await api.post("/auth/logout");
  },

  getCurrentUser: async (): Promise<AuthResponse["user"]> => {
    const { data } = await api.get("/auth/me");
    return data;
  },

  refreshToken: async (): Promise<{ token: string }> => {
    const { data } = await api.post("/auth/refresh");
    return data;
  },
};
