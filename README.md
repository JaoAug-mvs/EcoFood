# EcoFood

O EcoFood é um aplicativo que tem como objetivo reduzir o desperdício de alimentos, conectando estabelecimentos comerciais a consumidores de forma simples e eficiente.

Muitos restaurantes, padarias e mercados acabam descartando alimentos que ainda estão próprios para consumo, principalmente no final do dia. Ao mesmo tempo, existem pessoas interessadas em economizar e consumir de forma mais consciente.

O EcoFood surge como uma solução para esse problema. Através do aplicativo, os estabelecimentos podem disponibilizar esses alimentos com preços reduzidos, enquanto os usuários podem visualizar ofertas próximas, reservar os produtos e realizar a retirada diretamente no local.

O diferencial do aplicativo está na simplicidade e no propósito. Não há sistema de entrega — o foco é na reserva e retirada, o que reduz custos e torna o processo mais rápido e acessível. Além disso, o aplicativo incentiva o consumo sustentável, ajudando a diminuir o desperdício e promovendo um impacto positivo no meio ambiente.

Com uma interface moderna e intuitiva, o EcoFood oferece uma experiência prática para o usuário, permitindo que ele economize dinheiro ao mesmo tempo em que contribui para uma causa importante.

Em resumo, o EcoFood não é apenas um aplicativo de ofertas, mas uma plataforma que une economia, praticidade e responsabilidade ambiental.

---

## Funcionalidades

- **Splash & Onboarding** — apresentação do app em 3 slides com carrossel
- **Home** — ofertas próximas, filtro por categoria, busca e toggle de favoritos
- **Explorar** — listagem completa de produtos com filtros e busca
- **Detalhe do produto** — imagem, desconto, informações de retirada, restaurante
- **Fluxo de reserva** — seleção de quantidade → confirmação → comprovante com código de reserva
- **Pedidos** — histórico e reservas ativas com status e código de retirada
- **Favoritos** — pratos e estabelecimentos salvos
- **Perfil** — dados do usuário, impacto ambiental e atalhos de navegação

---

## Tecnologias

| Item | Versão |
|---|---|
| .NET MAUI | net10.0 |
| C# | 13 |
| CommunityToolkit.Mvvm | 8.4.0 |
| Padrão | MVVM + Injeção de Dependência |
| Plataformas | Windows 10+ · Android 5.0+ (API 21+) |

---

## Pré-requisitos

- ](h[.NET 10 SDKttps://dotnet.microsoft.com/download/dotnet/10.0)
- [Visual Studio 2022 17.12+](https://visualstudio.microsoft.com/) com a carga de trabalho **.NET Multi-platform App UI**  
  _ou_ VS Code com a extensão C# Dev Kit + MAUI
- Para Android: Android SDK instalado pelo Visual Studio (API 21+)
- Para Windows: Windows 10 versão 1809 (build 17763) ou superior

---

## Como executar

### Windows

```bash
dotnet build -f net10.0-windows10.0.19041.0
dotnet run -f net10.0-windows10.0.19041.0
```

Ou pelo Visual Studio: selecione o perfil **Windows Machine** e pressione `F5`.

### Android (emulador ou dispositivo físico)

```bash
dotnet build -f net10.0-android
dotnet run -f net10.0-android
```

Pelo Visual Studio: selecione um emulador Android ou dispositivo conectado via USB (com depuração USB ativada) e pressione `F5`.

---

## Estrutura do projeto

```
EcoFood/
├── Models/             # Entidades de dados (Product, Order, Restaurant, AppUser…)
├── Services/           # Interfaces e implementações mock (dados fictícios em memória)
├── ViewModels/         # Lógica de apresentação com MVVM Toolkit
│   └── Base/           # Utilitários compartilhados (formatação de moeda/distância)
├── Views/              # Páginas XAML + code-behind
├── Resources/
│   ├── Fonts/          # OpenSans Regular e Semibold
│   ├── Images/         # Ícones e logos
│   ├── Splash/         # Tela de splash nativa
│   ├── AppIcon/        # Ícone do app
│   └── Styles/         # Colors.xaml e Styles.xaml (tema global)
├── Platforms/          # Código específico por plataforma (Windows, Android)
├── App.xaml            # Recursos globais da aplicação
├── AppShell.xaml       # Navegação por tabs (5 abas)
└── MauiProgram.cs      # Configuração de DI, fontes e serviços
```

---

## Arquitetura

O projeto segue o padrão **MVVM** com injeção de dependência nativa do .NET:

- **Models** — dados puros, sem lógica de UI
- **Services** — acesso a dados (atualmente mock em memória, preparado para troca por API)
- **ViewModels** — estado e comandos expostos via `[ObservableProperty]` e `[RelayCommand]` do CommunityToolkit.Mvvm
- **Views** — XAML com bindings compilados (`x:DataType`) para melhor performance

A navegação principal usa **Shell** com 5 tabs. O fluxo de reserva (Detalhe → Reserva → Confirmação → Sucesso) usa rotas modais registradas em `AppShell.xaml.cs`. As telas de Splash e Onboarding trocam a `Window.Page` diretamente, sem passar pelo Shell.

---

## Observações

- Todos os dados são **fictícios e em memória** — nenhuma chamada de rede é feita
- O estado de favoritos e pedidos é mantido durante a sessão e resetado ao reiniciar o app
- O app foi desenvolvido e testado principalmente no **Windows**; o Android está configurado e compila, mas pode precisar de ajustes visuais
