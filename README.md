# KpiSessionSimulator



\## Архітектура проєкту



```mermaid

graph TD

&#x20;   UI\[KpiSession.UI<br/>Головний застосунок / Spectre.Console] --> Core

&#x20;   UI --> Models

&#x20;   

&#x20;   Core\[KpiSession.Core<br/>Бізнес-логіка / Патерни / Міні-ігри] --> Models

&#x20;   Core --> Data

&#x20;   

&#x20;   Data\[KpiSession.Data<br/>Збереження у JSON] --> Models

&#x20;   

&#x20;   Tests\[KpiSession.Tests<br/>Unit-тести / NUnit] -. Тестує .-> Core

&#x20;   Tests -. Тестує .-> Models

&#x20;   

&#x20;   Models\[KpiSession.Models<br/>Сутності: Питання, Гравець, Статистика]



&#x20;   classDef default fill:#f9f9f9,stroke:#333,stroke-width:2px;

&#x20;   classDef core fill:#d4edda,stroke:#28a745,stroke-width:2px;

&#x20;   classDef ui fill:#cce5ff,stroke:#007bff,stroke-width:2px;

&#x20;   classDef data fill:#fff3cd,stroke:#ffc107,stroke-width:2px;

&#x20;   classDef models fill:#e2e3e5,stroke:#6c757d,stroke-width:2px;



&#x20;   class Core core;

&#x20;   class UI ui;

&#x20;   class Data data;

&#x20;   class Models models;

