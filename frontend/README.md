# OrionDev Frontend

Frontend da plataforma OrionDev - Sistema de aprendizado gamificado para desenvolvedores.

## 🏗️ Arquitetura

Este projeto segue o padrão **MVVM (Model-View-ViewModel)** usando React Hooks como ViewModels.

### Estrutura de Pastas

```
src/
├── components/       # Componentes reutilizáveis (Views puras)
├── pages/           # Páginas/Rotas (Views compostas)
├── hooks/           # Custom Hooks (ViewModels)
├── services/        # Chamadas API e lógica de comunicação
├── types/           # TypeScript types e interfaces
│   ├── enums/      # Enumerações
│   ├── models/     # Interfaces de modelos
│   └── dtos/       # Data Transfer Objects
└── utils/           # Funções utilitárias puras
```

## 🚀 Tecnologias

- **React 19** - Biblioteca UI
- **TypeScript** - Tipagem estática
- **Vite** - Build tool e dev server
- **React Router** - Roteamento
- **TanStack Query (React Query)** - Gerenciamento de estado
- **Axios** - Cliente HTTP
- **Tailwind CSS** - Estilização

## 📋 Pré-requisitos

- Node.js 18+
- pnpm (recomendado) ou npm
- Backend OrionDev rodando em http://localhost:5100

## 🔧 Instalação

1. Instale as dependências:

```bash
pnpm install
```

2. Configure as variáveis de ambiente:

```bash
cp .env.example .env
```

## 🏃 Executando o Projeto

### Modo Desenvolvimento

```bash
pnpm dev
```

O projeto estará disponível em: http://localhost:5173

### Build para Produção

```bash
pnpm build
```

## 🔌 Conectando com o Backend

1. Certifique-se de que o backend está rodando:

```bash
cd ../backend
dotnet run --project OrionDev.AdminAPI
```

2. O backend deve estar disponível em: http://localhost:5100

## 📚 Principais Rotas

- `/` - Página inicial
- `/missions` - Lista de missões
- `/missions/:slug` - Detalhes de uma missão
- `/solar-systems` - Lista de sistemas solares

## 📖 Documentação Adicional

- [React Query Docs](https://tanstack.com/query/latest)
- [React Router Docs](https://reactrouter.com)
- [Tailwind CSS Docs](https://tailwindcss.com)
