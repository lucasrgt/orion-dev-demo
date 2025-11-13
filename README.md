# OrionDev

Plataforma gamificada de aprendizado de tecnologia com temática espacial.

## 🛠️ Stack

**Backend:** .NET 9 + PostgreSQL + Clean Architecture  
**Frontend:** React 19 + TypeScript + TanStack Query + Zustand + Zod + Tailwind CSS

## 🚀 Execução Rápida

### Modo Desenvolvimento

```powershell
# Iniciar
.\start-dev.ps1

# Parar
.\stop-dev.ps1
```

Sobe PostgreSQL (Docker) + Backend + Frontend em modo desenvolvimento com hot-reload.

**Acesse:**

- **Frontend:** http://localhost:5173
- **Backend API:** http://localhost:5100/scalar
- **Database:** localhost:5432

### Modo Produção (Docker)

```powershell
# Iniciar
.\start-prod.ps1

# Parar
.\stop-prod.ps1
```

Sobe todo o stack (PostgreSQL + Backend + Frontend) em containers Docker.

**Acesse:**

- **Frontend:** http://localhost
- **Backend API:** http://localhost:5100/scalar
- **Database:** localhost:5432

**Comandos úteis:**

```powershell
# Ver logs
docker compose logs -f

# Parar containers
docker compose down

# Remover volumes (limpar banco)
docker compose down -v
```

## 📋 Arquitetura

### Frontend (MVVM Pattern)

- **Views:** Components + Pages
- **ViewModels:** Custom Hooks (useMissions, useAuth, etc.)
- **Model:** Services + Types
- **State:** React Query (server) + Zustand (client)

### Backend (Clean Architecture)

- **Domain:** Entities + Business Rules
- **Application:** Use Cases + DTOs
- **Infrastructure:** Data Access + External Services
- **API:** Controllers + Endpoints

## 👤 Autor

**Lucas Richard Garcia Tinoco** - [GitHub](https://github.com/lucasrgt)
