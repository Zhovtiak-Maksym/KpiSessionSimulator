# KpiSessionSimulator
## Архітектура проєкту

```mermaid
graph TD
    UI[KpiSession.UI: Головний застосунок / Spectre.Console] --> Core
    UI --> Models
    
    Core[KpiSession.Core: Бізнес-логіка / Патерни / Міні-ігри] --> Models
    Core --> Data
    
    Data[KpiSession.Data: Збереження у JSON] --> Models
    
    Tests[KpiSession.Tests: Unit-тести / NUnit] -. Тестує .-> Core
    Tests -. Тестує .-> Models
    
    Models[KpiSession.Models: Сутності - Питання, Гравець, Статистика];

