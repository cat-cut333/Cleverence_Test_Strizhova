# CleverenceTests

## 📋 Описание

Модульные и интеграционные тесты для всех трёх заданий тестового проекта.

**Фреймворк:** xUnit  
**Покрытие:** 34+ тестов

---

## 🧪 Структура тестов

```
CleverenceTests/
├── CleverenceTests.csproj
│
├── Task1_StringCompressionTests/
│   ├── RleCompressorTests.cs       # Тесты для RLE-компрессии
│   └── ValidationTests.cs          # Тесты для валидатора
│
├── Task2_ThreadSafeServerTests/
│   └── StaticServerTests.cs        # Тесты для потокобезопасного сервера
│
└── Task3_LogStandardizerTests/
    ├── Format1ParserTests.cs       # Тесты для парсера формата 1
    ├── Format2ParserTests.cs       # Тесты для парсера формата 2
    ├── StandardLogFormatterTests.cs # Тесты для форматтера
    ├── LogProcessorIntegrationTests.cs # Интеграционные тесты
    └── ProblemLoggerTests.cs       # Тесты для асинхронного логгера
```

---

## 📊 Покрытие тестами

| Компонент | Количество тестов | Что проверяется |
|-----------|-------------------|-----------------|
| **Task1** | | |
| `RleCompressor` | 6 | Сжатие, распаковка, валидация, null, empty |
| `DefaultStringValidator` | 6 | Валидные строки, невалидные, null, allowEmpty |
| **Task2** | | |
| `StaticServer` | 6 | GetCount, AddToCount, многопоточность, переполнение |
| **Task3** | | |
| `Format1Parser` | 9 | Валидные строки, уровни, ошибки формата |
| `Format2Parser` | 12 | Валидные строки, метод DEFAULT, ошибки формата |
| `StandardLogFormatter` | 5 | Форматирование, DEFAULT, null, разное кол-во знаков |
| `LogProcessor` (интеграционные) | 5 | Работа с файлами, смешанные данные, ошибки |
| `ProblemLogger` | 3 | Асинхронная запись, append, пустая строка |
| **Всего** | **34** | |

---

## 🚀 Запуск тестов

### Через командную строку

```bash
# Из корня решения
dotnet test

# Из папки тестового проекта
cd CleverenceTests
dotnet test
```

### Через Visual Studio

1. Откройте **Тест -> Выполнить все тесты** (Ctrl + R, A).
2. Результаты появятся в окне **Обозреватель тестов (Test Explorer)**.

---

## 📝 Пример теста

```csharp
[Fact]
public void Compress_ValidString_ReturnsCompressed()
{
    // Arrange
    var validator = new DefaultStringValidator();
    var compressor = new RleCompressor(validator);
    string input = "aaabbcccdde";

    // Act
    string result = compressor.Compress(input);

    // Assert
    Assert.Equal("a3b2c3d2e", result);
}
```

---

## 🔧 Добавление новых тестов

1. Создайте файл в соответствующей папке.
2. Добавьте класс с атрибутом `[Fact]` для каждого теста.
3. Используйте **AAA (Arrange-Act-Assert)** для читаемости.

```csharp
[Fact]
public void MyTest_Scenario_ExpectedResult()
{
    // Arrange
    // Act
    // Assert
}
```

---

## 📌 Важные замечания

- ✅ **Все тесты должны проходить** перед отправкой.
- ✅ **Тесты не должны зависеть друг от друга** (изолированы).
- ✅ **Интеграционные тесты** создают и удаляют временные файлы.
