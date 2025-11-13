# OrionDev - Script de Inicializacao
# Execute este script para rodar backend e frontend simultaneamente

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "  OrionDev - Modo Desenvolvimento" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

# Funcao para verificar se uma porta esta em uso
function Test-Port {
    param($Port)
    $connection = Test-NetConnection -ComputerName localhost -Port $Port -InformationLevel Quiet -WarningAction SilentlyContinue
    return $connection
}

# Verificar se Docker esta instalado
Write-Host "Verificando Docker..." -ForegroundColor Yellow
try {
    docker --version | Out-Null
    Write-Host "Docker encontrado!" -ForegroundColor Green
}
catch {
    Write-Host "ERRO: Docker nao esta instalado ou nao esta no PATH" -ForegroundColor Red
    Write-Host "Por favor, instale o Docker Desktop: https://www.docker.com/products/docker-desktop" -ForegroundColor Yellow
    exit 1
}

Write-Host ""
Write-Host "Verificando portas..." -ForegroundColor Yellow

if (Test-Port 5432) {
    Write-Host "Porta 5432 ja esta em uso - Database" -ForegroundColor Yellow
    Write-Host "   O banco pode ja estar rodando" -ForegroundColor Gray
}
else {
    Write-Host "Porta 5432 disponivel - Database" -ForegroundColor Green
}

if (Test-Port 5100) {
    Write-Host "Porta 5100 ja esta em uso - Backend" -ForegroundColor Red
    Write-Host "   O backend pode ja estar rodando ou outra aplicacao esta usando esta porta" -ForegroundColor Gray
    $continueBackend = Read-Host "   Deseja continuar mesmo assim? (s/n)"
    if ($continueBackend -ne "s") {
        exit
    }
}

if (Test-Port 5173) {
    Write-Host "Porta 5173 ja esta em uso - Frontend" -ForegroundColor Red
    Write-Host "   O frontend pode ja estar rodando ou outra aplicacao esta usando esta porta" -ForegroundColor Gray
    Write-Host "   O Vite tentara usar a porta 5174 automaticamente" -ForegroundColor Gray
}

Write-Host ""

# Iniciar banco de dados com Docker Compose
Write-Host "Iniciando PostgreSQL..." -ForegroundColor Yellow
Set-Location $PSScriptRoot
docker compose up -d db

Write-Host "Aguardando banco de dados ficar pronto..." -ForegroundColor Yellow
Start-Sleep -Seconds 5

# Verificar se o banco esta saudavel
$maxRetries = 10
$retry = 0
while ($retry -lt $maxRetries) {
    $dbHealth = docker inspect --format='{{.State.Health.Status}}' oriondev-db 2>$null
    if ($dbHealth -eq "healthy") {
        Write-Host "PostgreSQL pronto!" -ForegroundColor Green
        break
    }
    $retry++
    Write-Host "Aguardando... ($retry/$maxRetries)" -ForegroundColor Gray
    Start-Sleep -Seconds 2
}

if ($retry -eq $maxRetries) {
    Write-Host "ERRO: PostgreSQL nao ficou pronto a tempo" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Instalando dependencias do frontend (se necessario)..." -ForegroundColor Yellow
$frontendPath = Join-Path $PSScriptRoot "frontend"
Set-Location $frontendPath
if (-not (Test-Path "node_modules")) {
    pnpm install
}
Set-Location $PSScriptRoot

Write-Host ""
Write-Host "Iniciando Backend .NET..." -ForegroundColor Green
$backendPath = Join-Path $PSScriptRoot "backend"
$backend = Start-Process powershell -ArgumentList "-NoExit", "-Command", "Set-Location '$backendPath'; Write-Host 'Backend iniciado!' -ForegroundColor Green; dotnet run --project OrionDev.AdminAPI" -PassThru

Start-Sleep -Seconds 3

Write-Host "Iniciando Frontend React + Vite..." -ForegroundColor Green
$frontend = Start-Process powershell -ArgumentList "-NoExit", "-Command", "Set-Location '$frontendPath'; Write-Host 'Frontend iniciado!' -ForegroundColor Green; pnpm dev" -PassThru

Write-Host ""
Write-Host "==================================" -ForegroundColor Green
Write-Host "  OrionDev iniciado com sucesso!" -ForegroundColor Green
Write-Host "==================================" -ForegroundColor Green
Write-Host ""
Write-Host "Servicos:" -ForegroundColor Cyan
Write-Host "   Database: localhost:5432" -ForegroundColor White
Write-Host "   Backend:  http://localhost:5100" -ForegroundColor White
Write-Host "   Frontend: http://localhost:5173" -ForegroundColor White
Write-Host ""
Write-Host "Para parar os servicos:" -ForegroundColor Yellow
Write-Host "   1. Feche as janelas do PowerShell (Backend/Frontend)" -ForegroundColor Gray
Write-Host "   2. Execute: docker compose stop db" -ForegroundColor Gray
Write-Host ""
Write-Host "Pressione qualquer tecla para sair deste script (os servidores continuarao rodando)..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
