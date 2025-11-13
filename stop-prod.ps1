# OrionDev - Script para Parar Producao
# Execute este script para parar todos os containers

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "  OrionDev - Parar Producao" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

# Usar o docker-compose.yml na raiz do projeto
Set-Location $PSScriptRoot

Write-Host "Parando containers..." -ForegroundColor Yellow
docker compose down

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "Containers parados com sucesso!" -ForegroundColor Green
    Write-Host ""
    
    $removeVolumes = Read-Host "Deseja remover os volumes (dados do banco)? (s/n)"
    if ($removeVolumes -eq "s") {
        Write-Host "Removendo volumes..." -ForegroundColor Yellow
        docker compose down -v
        Write-Host "Volumes removidos!" -ForegroundColor Green
    }
}
else {
    Write-Host ""
    Write-Host "ERRO ao parar containers" -ForegroundColor Red
    exit 1
}
