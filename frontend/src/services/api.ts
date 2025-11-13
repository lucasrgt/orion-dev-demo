import axios from "axios";
import { isTokenExpired } from "@/utils/jwtHelper";
import toast from "react-hot-toast";

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "http://localhost:5100/api",
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true, // Important for cookies (refresh token)
});

let isRefreshing = false;
let failedQueue: Array<{
  resolve: (value?: unknown) => void;
  reject: (reason?: unknown) => void;
}> = [];

const processQueue = (error: Error | null, token: string | null = null) => {
  failedQueue.forEach((prom) => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });

  failedQueue = [];
};

api.interceptors.request.use(
  async (config) => {
    const token = localStorage.getItem("authToken");

    if (token) {
      // Check if token is expired
      if (isTokenExpired(token)) {
        if (!isRefreshing) {
          isRefreshing = true;

          try {
            const { data: newToken } = await axios.post(
              `${config.baseURL?.replace("/api", "")}/api/auth/refresh`,
              {},
              { withCredentials: true }
            );

            localStorage.setItem("authToken", newToken);
            config.headers.Authorization = `Bearer ${newToken}`;
            processQueue(null, newToken);
            isRefreshing = false;
            return config;
          } catch (error) {
            processQueue(error as Error, null);
            isRefreshing = false;
            // Limpar tudo
            localStorage.removeItem("authToken");
            localStorage.removeItem("auth-storage");
            toast.error("Sessão expirada. Faça login novamente.");
            setTimeout(() => {
              window.location.href = "/login";
            }, 100);
            return Promise.reject(error);
          }
        }

        // If already refreshing, wait for it to complete
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject });
        }).then((token) => {
          config.headers.Authorization = `Bearer ${token}`;
          return config;
        });
      }

      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    if (error.response?.status === 401 && !originalRequest._retry) {
      if (isRefreshing) {
        return new Promise((resolve, reject) => {
          failedQueue.push({ resolve, reject });
        })
          .then((token) => {
            originalRequest.headers.Authorization = `Bearer ${token}`;
            return api(originalRequest);
          })
          .catch((err) => {
            return Promise.reject(err);
          });
      }

      originalRequest._retry = true;
      isRefreshing = true;

      try {
        const { data: newToken } = await axios.post(
          `${api.defaults.baseURL?.replace("/api", "")}/api/auth/refresh`,
          {},
          { withCredentials: true }
        );

        localStorage.setItem("authToken", newToken);
        api.defaults.headers.common.Authorization = `Bearer ${newToken}`;
        originalRequest.headers.Authorization = `Bearer ${newToken}`;
        processQueue(null, newToken);
        isRefreshing = false;

        return api(originalRequest);
      } catch (refreshError) {
        processQueue(refreshError as Error, null);
        isRefreshing = false;
        // Limpar tudo
        localStorage.removeItem("authToken");
        localStorage.removeItem("auth-storage");
        toast.error("Sessão expirada. Faça login novamente.");
        setTimeout(() => {
          window.location.href = "/login";
        }, 100);
        return Promise.reject(refreshError);
      }
    }

    return Promise.reject(error);
  }
);
