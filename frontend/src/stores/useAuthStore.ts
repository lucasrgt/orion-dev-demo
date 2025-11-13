import { create } from "zustand";
import { persist } from "zustand/middleware";
import { decodeJwt } from "@/utils/jwtHelper";

interface User {
  id: string;
  name: string;
  email: string;
  role: string;
}

interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
  setAuth: (user: User, token: string) => void;
  clearAuth: () => void;
  isAdmin: () => boolean;
}

export const useAuthStore = create<AuthState>()(
  persist(
    (set, get) => ({
      user: null,
      token: null,
      isAuthenticated: false,
      setAuth: (user, token) => {
        localStorage.setItem("authToken", token);
        const decoded = decodeJwt(token);
        const userWithRole = {
          ...user,
          role: decoded?.role || "User",
        };
        set({ user: userWithRole, token, isAuthenticated: true });
      },
      clearAuth: () => {
        localStorage.removeItem("authToken");
        set({ user: null, token: null, isAuthenticated: false });
      },
      isAdmin: () => {
        const state = get();
        return state.user?.role === "Admin";
      },
    }),
    {
      name: "auth-storage",
      partialize: (state) => ({
        user: state.user,
        token: state.token,
        isAuthenticated: state.isAuthenticated,
      }),
    }
  )
);
