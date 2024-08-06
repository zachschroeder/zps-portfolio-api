# ZpsPortfolioApi

This project is the back-end of my personal portfolio project: https://zps-portfolio-web.azurewebsites.net/

## Testing
Code coverage can be obtained with the following commands

```powershell
# Go to correct directory
cd .\Portfolio.Test\

# Test and calculate coverage to XML
dotnet test --collect:"XPlat Code Coverage"

# Generate HTML report based on XML
reportgenerator -reports:".\TestResults\{InsertGuidHere}\coverage.cobertura.xml" -targetdir:"TestResults" -reporttypes:Html

# Open HTML in browser
.\TestResults\index.html
```
