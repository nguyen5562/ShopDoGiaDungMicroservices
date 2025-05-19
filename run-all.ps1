# Danh sách các thư mục service
$services = @("AccountServices", "ApiGateway", "AuthServices", "OrderServices", "OtherServices", "ProductServices")

# Chạy từng service trong một terminal riêng
foreach ($service in $services) {
    Write-Host "Starting $service..."
    Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\$service'; dotnet run"
}

Write-Host "All services started!"