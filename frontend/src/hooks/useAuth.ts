import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { authService } from "@/services/authService";
import { useAuthStore } from "@/stores";
import { loginSchema, registerSchema } from "@/schemas";
import type { LoginInput, RegisterInput } from "@/schemas";
import { useNavigate } from "react-router-dom";

export function useLogin() {
  const { setAuth } = useAuthStore();
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const {
    mutate: login,
    isPending,
    error,
  } = useMutation({
    mutationFn: async (credentials: LoginInput) => {
      const validated = loginSchema.parse(credentials);
      return authService.login(validated);
    },
    onSuccess: ({ user, token }) => {
      setAuth(user, token);
      queryClient.invalidateQueries({ queryKey: ["user"] });
      navigate("/");
    },
    onError: (error: any) => {
      console.error("Login failed:", error);
    },
  });

  return {
    login,
    isLoading: isPending,
    error,
  };
}

export function useRegister() {
  const { setAuth } = useAuthStore();
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const {
    mutate: register,
    isPending,
    error,
  } = useMutation({
    mutationFn: async (userData: RegisterInput) => {
      const validated = registerSchema.parse(userData);
      return authService.register(validated);
    },
    onSuccess: ({ user, token }) => {
      setAuth(user, token);
      queryClient.invalidateQueries({ queryKey: ["user"] });
      navigate("/");
    },
    onError: (error: any) => {
      console.error("Registration failed:", error);
    },
  });

  return {
    register,
    isLoading: isPending,
    error,
  };
}

export function useLogout() {
  const { clearAuth } = useAuthStore();
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const { mutate: logout, isPending } = useMutation({
    mutationFn: authService.logout,
    onSuccess: () => {
      clearAuth();
      queryClient.clear();
      navigate("/login");
    },
    onError: () => {
      // Mesmo com erro, faz logout local
      clearAuth();
      queryClient.clear();
      navigate("/login");
    },
  });

  return {
    logout,
    isLoading: isPending,
  };
}

export function useCurrentUser() {
  const { user, isAuthenticated } = useAuthStore();

  const { data, isLoading, error } = useQuery({
    queryKey: ["user"],
    queryFn: authService.getCurrentUser,
    enabled: isAuthenticated,
    staleTime: 1000 * 60 * 5, // 5 minutes
  });

  return {
    user: data || user,
    isLoading,
    error,
    isAuthenticated,
  };
}
