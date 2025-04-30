Feature: Autenticação de Usuários

  Scenario: Login com credenciais válidas
    Given que o usuário "admin" com senha "12345678" existe
    When o usuário tenta fazer login com "admin" e "12345678"
    Then a resposta deve conter um token JWT

  Scenario: Registro de novo usuário com dados válidos
    When um novo usuário tenta se registrar com username "maria", senha "senha1234" e email "maria@exemplo.com"
    Then a resposta deve retornar status 201 e os dados do usuário criado

  Scenario: Tentativa de login com senha inválida
    When o usuário tenta fazer login com "admin" e "senhaerrada"
    Then a resposta deve ser 401 Unauthorized
