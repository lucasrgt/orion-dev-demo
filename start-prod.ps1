# OrionDev - Script de Producao
# Execute este script para rodar o projeto completo com Docker Compose

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "  OrionDev - Modo Producao" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

# Verificar se Docker esta instalado
Write-Host "Verificando Docker..." -ForegroundColor Yellow
try {
    $dockerVersion = docker --version
    Write-Host "Docker encontrado: $dockerVersion" -ForegroundColor Green
}
catch {
    Write-Host "ERRO: Docker nao esta instalado ou nao esta no PATH" -ForegroundColor Red
    Write-Host "Por favor, instale o Docker Desktop: https://www.docker.com/products/docker-desktop" -ForegroundColor Yellow
    exit 1
}

# Verificar se Docker Compose esta disponivel
try {
    $composeVersion = docker compose version
    Write-Host "Docker Compose encontrado: $composeVersion" -ForegroundColor Green
}
catch {
    Write-Host "ERRO: Docker Compose nao esta disponivel" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Construindo e iniciando containers..." -ForegroundColor Cyan
Write-Host "Isso pode levar alguns minutos na primeira vez..." -ForegroundColor Gray
Write-Host ""

# Usar o docker-compose.yml na raiz do projeto
Set-Location $PSScriptRoot

# Parar containers existentes se houver
Write-Host "Parando containers existentes..." -ForegroundColor Yellow
docker compose down 2>$null

# Construir e iniciar containers
Write-Host "Iniciando servicos..." -ForegroundColor Yellow
docker compose up --build -d

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "==================================" -ForegroundColor Green
    Write-Host "  Aplicacao Iniciada com Sucesso!" -ForegroundColor Green
    Write-Host "==================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Servicos disponiveis:" -ForegroundColor Cyan
    Write-Host "  Frontend:  http://localhost" -ForegroundColor White
    Write-Host "  Backend:   http://localhost:5100" -ForegroundColor White
    Write-Host "  API Docs:  http://localhost:5100/scalar" -ForegroundColor White
    Write-Host "  Database:  localhost:5432" -ForegroundColor White
    Write-Host ""
    Write-Host "Para visualizar logs:" -ForegroundColor Yellow
    Write-Host "  docker compose logs -f" -ForegroundColor Gray
    Write-Host ""
    Write-Host "Para parar a aplicacao:" -ForegroundColor Yellow
    Write-Host "  docker compose down" -ForegroundColor Gray
    Write-Host ""
    
    # Mostrar logs em tempo real
    $showLogs = Read-Host "Deseja visualizar os logs em tempo real? (s/n)"
    if ($showLogs -eq "s") {
        Write-Host ""
        Write-Host "Exibindo logs (Ctrl+C para sair)..." -ForegroundColor Cyan
        docker compose logs -f
    }
}
else {
    Write-Host ""
    Write-Host "ERRO ao iniciar containers" -ForegroundColor Red
    Write-Host "Execute 'docker compose logs' para ver os detalhes" -ForegroundColor Yellow
    exit 1
}
