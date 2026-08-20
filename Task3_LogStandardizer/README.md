# Task3: Log Standardizer

## 📋 Описание

Консольная программа для стандартизации лог-файлов из двух форматов в единый табулированный формат.

### Входные форматы

| Формат | Разделитель | Пример |
|--------|-------------|--------|
| **Формат 1** | Пробел | `10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0'` |
| **Формат 2** | `\|` | `2025-03-10 15:14:51.5882\| INFO\|11\|Method\| Сообщение` |

### Выходной формат

```
DD-MM-YYYY\tHH:MM:SS.fff\tLEVEL\tMETHOD\tMESSAGE
```

---

## 🏗️ Архитектура

```
Task3_LogStandardizer/
├── Core/
│   ├── Entities/               # LogEntry
│   ├── Enums/                  # LogLevel
│   ├── Abstract/               # LogParserBase
│   └── Interfaces/             # ILogParser, ILogFormatter
├── Parsers/                    # Format1Parser, Format2Parser
├── Formatters/                 # StandardLogFormatter
├── Services/                   # LogProcessor
├── Infrastructure/             # ProblemLogger
└── Program.cs                  # Точка входа
```

---

## 🔄 Процесс обработки

```
Входной файл
    ↓
Чтение строк
    ↓
Парсинг (Format1Parser / Format2Parser)
    ↓
Создание LogEntry
    ↓
Форматирование (StandardLogFormatter)
    ↓
Запись в выходной файл
    ↓
(если ошибка) → ProblemLogger → problems.txt
```

---

## 🧪 Тесты

| Компонент | Количество тестов |
|-----------|-------------------|
| `Format1Parser` | 9 |
| `Format2Parser` | 12 |
| `StandardLogFormatter` | 5 |
| `LogProcessor` (интеграционные) | 5 |
| `ProblemLogger` | 3 |
| **Всего** | **34** |

**Запуск тестов:**
```bash
dotnet test
```

---

## 🚀 Запуск

```bash
cd Task3_LogStandardizer
dotnet run -- input.txt output.txt problems.txt
```

### Аргументы командной строки

| Аргумент | Описание | По умолчанию |
|----------|----------|--------------|
| `args[0]` | Входной файл | `input.txt` |
| `args[1]` | Выходной файл | `output.txt` |
| `args[2]` | Файл проблем | `problems.txt` |

---

## 📝 Ключевые особенности

- ✅ **Сохранение формата времени** — время выводится в исходном виде.
- ✅ **Асинхронная запись** — проблемные строки записываются без блокировки.
- ✅ **Расширяемость** — легко добавить новый формат логов (наследование от `LogParserBase`).
- ✅ **Обработка ошибок** — невалидные строки попадают в `problems.txt`.
