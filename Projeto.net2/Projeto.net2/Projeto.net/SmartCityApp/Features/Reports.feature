Feature: Listagem de Relatórios

  Scenario: Obter lista de relatórios com paginação válida
    Given que existem relatórios cadastrados no sistema
    When o usuário solicita a lista de relatórios na página 1 com pageSize 10
    Then a resposta deve retornar status 200 e uma lista de relatórios

  Scenario: Obter lista de relatórios com parâmetros inválidos
    When o usuário solicita a lista de relatórios com page 0 e pageSize -5
    Then a resposta deve retornar status 400 com mensagem de erro apropriada
