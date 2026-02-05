# MyColl - Plataforma de Colecionáveis Multiplataforma

**MyColl** é uma solução integrada desenvolvida para a gestão e comercialização de itens colecionáveis (moedas, selos, camisolas, etc.). O projeto utiliza o ecossistema **.NET 8** para oferecer uma experiência consistente tanto na Web como em dispositivos móveis, garantindo segurança e escalabilidade.

---

## Arquitetura do Sistema

O projeto baseia-se num modelo de **desacoplamento**, dividido em três componentes principais:

1.  **Frontend Multiplataforma (RCL + Web + MAUI):**
    *   **RCL (Razor Class Library):** Centraliza toda a interface de utilizador (UI) e lógica de cliente.
    *   **Blazor Web:** Host para acesso via browser.
    *   **Blazor Hybrid (.NET MAUI):** Aplicação nativa para Windows, Android, MacOS, etc.
2.  **Backend (API RESTful):**
    *   Construída em **ASP.NET Core Web API**, serve como intermediária entre os clientes e a base de dados.
    *   Implementa segurança via **JWT (JSON Web Tokens)** com Claims de Roles.
3.  **Base de Dados (SQL Server):**
    *   Persistência de dados utilizando **Entity Framework Core 8** com a abordagem *Code-First*.

---

## Funcionalidades Principais

### Públicos e Perfis
*   **Utilizador Anónimo:** Navegação no catálogo, pesquisa avançada e visualização de produtos em destaque.
*   **Cliente:** Gestão de carrinho de compras e checkout simulado..
*   **Fornecedor:** Gestão de inventário próprio.
*   **Administrador:** Controlo total de utilizadores (aprovação de contas), gestão de categorias, definição de taxas de lucro e logística de encomendas.

### Destaques Técnicos
*   **Pesquisa em Cascata:** Filtros reativos que atualizam subcategorias dinamicamente conforme a categoria mãe selecionada.
*   **Upload de Imagens por Chunks:** Sistema robusto de upload de ficheiros binários via SignalR, processados em blocos de 512KB para garantir estabilidade da ligação.
*   **Regras de Negócio Automatizadas:** Cálculo automático do preço final (Taxa Admin) e abate de stock no momento da expedição.
*   **Segurança Híbrida:** Autenticação via Cookies (Web) e LocalStorage (Mobile), unificada por um motor JWT na API.

---

#### Execução Multi-Projeto
Para o funcionamento correto, o Visual Studio deve iniciar simultaneamente:
*   RESTfulAPIMYCOLL (Backend API)
*   MYCOLL.Web (Frontend Web)
*   MYCOLL.MAUI (Frontend App)
*   MYCOLL (Backoffice/Gestão)

---

## Autores
- [Fábio Oliveira](https://github.com/fabiooliv3ira)
- [Sebastian Gonçalves](https://github.com/sebie12)
