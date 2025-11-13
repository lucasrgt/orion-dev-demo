import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { Toaster } from 'react-hot-toast';
import { Layout } from '@/components/Layout';
import { ProtectedRoute, PublicRoute } from '@/components/ProtectedRoute';
import { HomePage } from '@/pages/HomePage';
import { MissionsPage } from '@/pages/MissionsPage';
import { MissionDetailPage } from '@/pages/MissionDetailPage';
import { SolarSystemsPage } from '@/pages/SolarSystemsPage';
import { PlanetsPage } from '@/pages/PlanetsPage';
import { PlanetDetailPage } from '@/pages/PlanetDetailPage';
import { MoonDetailPage } from '@/pages/MoonDetailPage';
import { LoginPage } from '@/pages/LoginPage';
import { RegisterPage } from '@/pages/RegisterPage';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: 1000 * 60 * 5, // 5 minutes
      retry: 1,
      refetchOnWindowFocus: false,
    },
  },
});

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Toaster
        position="top-right"
        toastOptions={{
          duration: 3000,
          style: {
            background: 'rgb(51, 65, 85)',
            color: '#fff',
          },
          success: {
            iconTheme: {
              primary: '#10b981',
              secondary: '#fff',
            },
          },
          error: {
            iconTheme: {
              primary: '#ef4444',
              secondary: '#fff',
            },
          },
        }}
      />
      <BrowserRouter>
        <Routes>
          {/* Public routes */}
          <Route element={<PublicRoute />}>
            <Route path="/login" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
          </Route>

          {/* Layout routes - all pages require authentication */}
          <Route element={<Layout />}>
            <Route element={<ProtectedRoute />}>
              <Route path="/" element={<HomePage />} />
              <Route path="/missions" element={<MissionsPage />} />
              <Route path="/missions/:slug" element={<MissionDetailPage />} />
              <Route path="/solar-systems" element={<SolarSystemsPage />} />
              <Route path="/planets" element={<PlanetsPage />} />
              <Route path="/planets/:id" element={<PlanetDetailPage />} />
              <Route path="/moons/:id" element={<MoonDetailPage />} />
            </Route>
          </Route>
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}

export default App;
