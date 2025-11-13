# OrionDev - Script para Parar Desenvolvimento
# Execute este script para parar o banco de dados

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "  OrionDev - Parar Desenvolvimento" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan
Write-Host ""

Set-Location $PSScriptRoot

Write-Host "Parando banco de dados..." -ForegroundColor Yellow
docker compose stop db

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "Banco de dados parado com sucesso!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Lembre-se de fechar as janelas do PowerShell do Backend e Frontend" -ForegroundColor Gray
    Write-Host ""
    
    $removeVolumes = Read-Host "Deseja remover os dados do banco? (s/n)"
    if ($removeVolumes -eq "s") {
        Write-Host "Removendo container e volumes..." -ForegroundColor Yellow
        docker compose down -v
        Write-Host "Dados removidos!" -ForegroundColor Green
    }
} else {
    Write-Host ""
    Write-Host "ERRO ao parar banco de dados" -ForegroundColor Red
    exit 1
}
