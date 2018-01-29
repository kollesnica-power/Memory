# Project guidelines

## Общие рекомендации

__Следует:__

  1. Внимательно относится к __рекомендациям статического анализатора__ Android Studio  (warnings и errors). Приветствуется дополнительное использование сторонних анализаторов, таких как `CheckStyle`, `FindBugs` и т.д.;
  2. Использовать горячую клавишу __`Ctrl + Alt + L`__ для автоматического форматирования кода;
  3. Отказаться от использования __Deprecated-методов__, а также стараться их заменять на актуальные;
  4. Осторожно подходить к __подключению сторонних библиотек__. Необходимо убедиться в том, что лицензия позволяет ее использование в коммерческих целях, изучить исходных код, оценить обоснованность добавления (возможно аналогичный функционал уже реализован в используемых библиотеках);
  5. Выносить функционал в __отдельный модуль__, если он логически отделим, изолирован, а также выполняет одну функциональную задачу;
  6. Добавлять __правила для Proguard__ при подключении сторонних библиотек (если требуется);
  7. Крайне внимательно следить за __изменением размера apk__, при добавлении raw-данных/ресурсов/библиотек;
  8. Стараться создавать компоненты, которые можно было бы легко переиспользовать в другом месте __(Особенно UI)__;
  9. Не блочить UI-поток, например, используя `sleep(int time)`;
  10. Не использовать `AsyncTask` (считать, что он `Deprecated`)! Использовать `RxJava`, либо, если очень нужно, создавать `Thread` вручную;
  11. Крайне осторожно работать с ссылками на Activity/Context, для избежания утечек памати использовать `WeakReference<T>`. Для анализа утечек можно использовать библиотеку `Leakcanary`;

## 1 Android code style
## 1.1 Структура проекта

Все добавляемые новые модули проекта должны следовать структуре, которая определена в [Android Gradle plugin user guide](http://tools.android.com/tech-docs/new-build-system/user-guide#TOC-Project-Structure).

Tip: перед тем, как приступать к добавлению нового функционала или абсолютно новой фичи, пожалуйста, ознакомься с [KISS-principle](http://web.archive.org/web/20160206225831/https://people.apache.org/~fhanik/kiss.html) и со статьей [Write code that is easy to delete, not easy to extend](http://programmingisterrible.com/post/139222674273/write-code-that-is-easy-to-delete-not-easy-to). Это чтение на 5 минут, но, возможно, поможет по-новому взглянуть на принципы проектирования основы для нового функционала.

#### Как создать новый Fragment/Activity, следуя принципам архитектурного паттерна MVP:
(О том, что такое MVP можно почитать [тут](https://ru.wikipedia.org/wiki/Model-View-Presenter).
О том, как применять MVP в андроид, можно начать читать [тут](http://antonioleiva.com/mvp-android/), затем можно продолжить [тут](http://fernandocejas.com/2014/09/03/architecting-android-the-clean-way/))

Предположим, что тебе нужно создать новый экран с новой фичей.

Создай новый пакет в пакете `gui`, назови его `coolfeature` (по названию твоей фичи).
Создай новую Activity (или Fragment), назови её `CoolFeatureActivity`.
Создай новый View interface, который будет имплементировать твоя Activity. Назови свой новый Interface `CoolFeatureView`.
Создай новый Presenter, назови его `CoolFeaturePresenter`.

Таким образом, должно получиться следующее:

Активити. Не забудь создать в ней Presenter:
```
/**
 * Cool Stuff Activity.
 */
public class CoolFeatureActivity extends BaseActivity implements CoolFeatureView {

}
```


Presenter для Activity:
```
/**
 * Presenter for {@link CoolFeatureActivity}
 */
public class CoolFeaturePresenter extends BasePresenter<CoolFeatureView>  {
    ...
}
```


View Interface для Activity:
```
/**
 * A view for {@link CoolFeatureActivity}
 */
public interface CoolFeatureView extends MvpView  {
    ...
}
```


## 1.2 Названия файлов

### 1.2.1 Названия классов
Названия классов должны быть написаны в [UpperCamelCase](http://en.wikipedia.org/wiki/CamelCase).

Для классов, которые наследуются от какого-либо Android component, название класса должно заканчиваться названием компонента, например: `SignInActivity`, `SignInFragment`, `ImageUploaderService`, `ChangePasswordDialog`.

### 1.2.2 Файлы Resources

Названия файлов Resources должны быть написаны в __lowercase_underscore__. При этом наличие __underscore__ символов в начале или в конце названия файла не допускается.

Так плохо: ```__drm_activity_awesome_feature__.xml```

Так хорошо: ```activity_awesome_feature.xml```

#### 1.2.2.1 Файлы Drawable

Преимущественно использовать векторную графику. Online converter: [Android SVG to VectorDrawable](http://inloop.github.io/svg2android/).

Правила именования файлов drawables:

| Asset Type   | Prefix             |   Example                     |
|--------------| -------------------|-------------------------------|
| Action bar   | `ab_`              | `ab_stacked.9.png`            |
| Button       | `btn_`             | `btn_send_pressed.9.png`      |
| Dialog       | `dialog_`          | `dialog_top.9.png`            |
| Divider      | `divider_`         | `divider_horizontal.9.png`    |
| Icon         | `ic_`              | `ic_star.png`                 |
| Menu         | `menu_	`            | `menu_submenu_bg.9.png`       |
| Notification | `notification_`    | `notification_bg.9.png`       |
| Tabs         | `tab_`             | `tab_pressed.9.png`           |
| Vector       | `v_`               | `v_ic_start.xml`              |


Правила именования файлов icons (взято из [Android iconography guidelines](http://developer.android.com/design/style/iconography.html)):

| Asset Type                      | Prefix             | Example                      |
| --------------------------------| ----------------   | ---------------------------- |
| Icons                           | `ic_`              | `ic_star.png`                |
| Launcher icons                  | `ic_launcher`      | `ic_launcher_calendar.png`   |
| Menu icons and Action Bar icons | `ic_menu`          | `ic_menu_archive.png`        |
| Status bar icons                | `ic_stat_notify`   | `ic_stat_notify_msg.png`     |
| Tab icons                       | `ic_tab`           | `ic_tab_recent.png`          |
| Dialog icons                    | `ic_dialog`        | `ic_dialog_info.png`         |


Правила именования для selector states:

| State	        | Suffix          | Example                     |
|--------------|-----------------|-----------------------------|
| Normal       | `_normal`       | `btn_order_normal.9.png`    |
| Pressed      | `_pressed`      | `btn_order_pressed.9.png`   |
| Focused      | `_focused`      | `btn_order_focused.9.png`   |
| Disabled     | `_disabled`     | `btn_order_disabled.9.png`  |
| Selected     | `_selected`     | `btn_order_selected.9.png`  |

#### 1.2.2.2 Файлы Layout

Layout файлы должны соответствовать названию Android компонентов, для которых они предназначены, но название компонента необходимо перенести в начало. Например, если мы создаем layout для `SignInActivity`, название layout файла должно быть `activity_sign_in.xml`.

| Component        | Class Name             | Layout Name                   |
| ---------------- | ---------------------- | ----------------------------- |
| Activity         | `UserProfileActivity`  | `activity_user_profile.xml`   |
| Fragment         | `SignUpFragment`       | `fragment_sign_up.xml`        |
| Dialog           | `ChangePasswordDialog` | `dialog_change_password.xml`  |
| AdapterView item | ---                    | `item_person.xml`             |
| Partial layout   | ---                    | `partial_stats_bar.xml`       |
| CustomView       | `SideBarView`          | `view_side_bar.xml`           |

Немного другой случай, когда мы создаем layout, который будет инфлейтиться `Adapter'ом`, В этом случае, название layout'а должно начинаться с `item_`.

Обрати внимание, что возможны случаи, при которых эти правила будет невозможно применить. Например, когда создаются layout-файлы, которые будут являться частью других layout'ов. В этом случае, ты должен использовать префикс `partial_`.

#### 1.2.2.3 Файлы Menu

Также, как и layout-файлы, файлы menu должны соответствовать названию компонента. Например, если мы определяем Menu-файл, который будет использоваться в `UserActivity`, то название файла должно быть `activity_user.xml`

#### 1.2.2.4 Файлы Values

Файлы Resource в пакете values должны быть __во множественном числе__, например, `strings.xml`, `styles.xml`, `colors.xml`, `dimens.xml`, `attrs.xml`

#### 1.2.2.5 Конфигурационные файлы

Конфигурационные файлы должны лежать в корне проекта и иметь следующий формат `<имяконфигурации>.properites`, например, `version.properties`.


## 2 Code guidelines
## 2.1 Правила языка Java
### 2.1.1 Никогда не игнорируй исключения!

Ты никогда не должен делать следующее:

```
void setServerPort(String value) {
    try {
        serverPort = Integer.parseInt(value);
    } catch (NumberFormatException e) { }
}
```

_Пока ты считаешь, что твой код никогда не столкнется с таким исключением, или что совершенно необязательно что-то с ним делать, игнорирование таких ошибок закладывает мины в твоем коде для кого-то другого. Ты должен обрабатывать каждую ошибку в своем коде. Специфика обработки ощибок может отличаться, в зависимости от конкретной задачи._ - ([Android code style guidelines](https://source.android.com/source/code-style.html))

Почитай об этом также [тут](https://source.android.com/source/code-style.html#dont-ignore-exceptions).

### 2.1.2 Don't catch generic exception

Никогда так не делай:

```
try {
    someComplicatedIOFunction();        // may throw IOException
    someComplicatedParsingFunction();   // may throw ParsingException
    someComplicatedSecurityFunction();  // may throw SecurityException
    // phew, made it all the way
} catch (Exception e) {                 // I'm so cool, I'll just catch all exceptions
    handleError();                      // with one generic handler!
}
```

Почему так делать нельзя и какие есть варианты смотри [тут](https://source.android.com/source/code-style.html#dont-catch-generic-exception)

### 2.1.3 Don't use finalizers

_Не используй finalizers. Подробно об этом тут:_ - ([Android code style guidelines](https://source.android.com/source/code-style.html#dont-use-finalizers))


### 2.1.4 Полностью прописывай импорты

Так делать плохо: `import foo.*;`

А вот так хорошо: `import foo.Bar;`

Почитай об этом подробно [тут](https://source.android.com/source/code-style.html#fully-qualify-imports)

## 2.2 Java style rules

### 2.2.1 Fields definition and naming (Именование и определение полей классов)

Fields должны быть определены в __самом начале файла__, и они должны следовать следующим правилам.

* Private, non-static field names start with __m__.
* Private, static field names start with __s__.
* Other fields start with a lower case letter.
* Static final fields (constants) are ALL_CAPS_WITH_UNDERSCORES.

Есть различные мнения относительно использования [__hungarian notation__](https://ru.wikipedia.org/wiki/%D0%92%D0%B5%D0%BD%D0%B3%D0%B5%D1%80%D1%81%D0%BA%D0%B0%D1%8F_%D0%BD%D0%BE%D1%82%D0%B0%D1%86%D0%B8%D1%8F) в Android программировании.

Известные в узких кругах противники __hungarian notation__ [утверждают](http://jakewharton.com/just-say-no-to-hungarian-notation/), что Android Studio и так различным образом подсвечивает различные типы переменных и нет необходимости в введении дополнительных правил. Что верно в случае чтения кода в IDE. Но при работе с pull-requests, когда предполагается чтение кода вне среды разработки, отсутствие __hungarian notation__ усложняет читаемость кода.

Пример:

```
public class MyClass {
    public static final int SOME_CONSTANT = 42;
    public int publicField;
    private static MyClass sSingleton;
    int mPackagePrivate;
    private int mPrivate;
    protected int mProtected;
}
```

### 2.2.3 Использование аббревиатур в качестве названий

| Хорошо           | Плохо            |
| ---------------- | ---------------- |
| `XmlHttpRequest` | `XMLHTTPRequest` |
| `getCustomerId`  | `getCustomerID`  |
| `String url`     | `String URL`     |
| `long id`        | `long ID`        |

### 2.2.4 Использование пробелов

Используй __4 пробела__ для отступов:

```
if (x == 1) {
    x++;
}
```

Используй __8 пробелов__ для разрывов строк:

```
Instrument i =
        someLongExpression(that, wouldNotFit, on, one, line);
```

__Note:__ Android Studio делает всю работу за тебя, ты, конечно же, можешь (и даже должен) использовать табы. Но всегда помни, что расставлять пробелы рандомно - это плохо!

### 2.2.5 Используй standard brace style

Фигурные скобки находятся на той же самой строке, что и код перед ними. Перед фигурной скобкой __всегда__ ставится пробел. После фигурной скобки также всегда ставится пробел, если предполагается новый __statement__. Пробел также ставится перед и после каждого оператора (*, %, +, - и т. д.)
```
// Так делать плохо:
class MyClass{
    int func(){
        if (x!=y+getValue(someValue)){
            // ...
        }else if (somethingElse){
            // ...
        }else{
            // ...
        }
    }
}
```

```
// Так тоже плохо:
class MyClass
{
    int func()
    {
        if (x != y + getValue(someValue))
        {
            // ...
        }
        else if (somethingElse)
        {
            // ...
        }
        else
        {
            // ...
        }
    }
}
```

```
// Так хорошо:
class MyClass {
    int func() {
        if (x != y + getValue(someValue)) {
            // ...
        } else if (somethingElse) {
            // ...
        } else {
            // ...
        }
    }
}
```

Если ты сомневаешься в своей способности запомнить определенные выше правила, __в Android Studio есть горячая клавиша ```Ctrl+Alt+L```__, при использовании которой автоматически форматируется класс, в котором ты находишься.

Фигурные скобки вокруг statements необходимы всегда, за исключением случаев, когда condition и body находятся на одной строке.

Если condition и body помещаются на одной строке и эта строка короче максимальной длины строки, тогда фигурные скобки не нужны, т. е.

```
if (condition) body();
```

Так делать __плохо__:

```
if (condition)
    body();  // bad!
```

### 2.2.6 Аннотации

#### 2.2.6.1 Annotations practices

В соответствии с Android code style guide, для некоторых предопределенных аннотаций в Java стандартной является такая практика:

* `@Override`: Аннотация @Override __должна быть использована всегда__ когда метод переопределяет declaration или implementation из супер-класса.

* `@SuppressWarnings`: Аннотация @SuppressWarnings должна использоваться только в тех случаях, когда невозможно избежать warning'а.

Больше информации по annotation guidelines читай [тут](http://source.android.com/source/code-style.html#use-standard-java-annotations).

#### 2.2.6.2 Annotations style

__Classes, Methods and Constructors__

Когда аннотации применяются к классу, методу или конструктору, они располагаются после блока документации и должны располагаться, как __одна аннотация на строке__.

```
/* This is the documentation block about the class */
@AnnotationA
@AnnotationB
public class MyAnnotatedClass { }
```

Рекомендуется помечать методы (__особенно, которые явно могут возвращать в качестве значения Null__) и поля аннотациями `@Nullable` и `@NonNull`. Это сильно упрощает поддержку кода.

### 2.2.7 Limit variable scope

_Скоуп локальных переменных необходимо сводить к минимуму (Effective Java Item 29). Таким образом, ты улучшаешь читаемость и поддерживаемость своего кода и уменьшаешь вероятность ошибок. Каждая переменная должна быть декларирована внутри того блока, где она используется._

_Локальные переменные должны быть задекларированы в той последовательности, в которой они инициализируются. Если у тебя пока недостаточно информации, чтобы здраво определить, когда переменная будет инициализирована, ты должен отложить декларирование переменной и вернуться к этому вопросу позже._ - ([Android code style guidelines](https://source.android.com/source/code-style.html#limit-variable-scope))

### 2.2.8 Порядок import statement'ов

Если ты используешь IDE, надеюсь, что это Android Studio. В этом случае, тебе не нужно беспокоиться об импортах, т. к. Android Studio в большинстве случаев полностью управляет этим процессом. Если же нет, читай ниже (но лучше установи себе Android Studio).

Порядок import statement'ов такой:

1. Android imports
2. Imports from third parties (com, junit, net, org)
3. java and javax
4. Same project imports

Чтобы полностью соответствовать настройкам IDE, импорты должны быть:

* В алфавитном порядке внутри каждой группы, с заглавными буквами перед строчными (т. е. Z перед a).
* Должна быть пустая строка между каждой группой (android, com, junit, net, org, java, javax).

Больше информации [тут](https://source.android.com/source/code-style.html#limit-variable-scope)

### 2.2.9 Logging guidelines

Для логирования используй методы, предоставляемые `Log` классом, чтобы выводить сообщения об ошибках или другую информацию, которая может быть полезна другим девелоперам, чтобы определить проблему:

* `Log.v(String tag, String msg)` (verbose)
* `Log.d(String tag, String msg)` (debug)
* `Log.i(String tag, String msg)` (information)
* `Log.w(String tag, String msg)` (warning)
* `Log.e(String tag, String msg)` (error)

В качестве главного правила, используй имя класса, как имя тэга и определяй его, как `static final` field в самом начале файла. Например:

```
public class MyClass {
    private static final String TAG = MyClass.class.getSimpleName();

    public myMethod() {
        Log.e(TAG, "My error message");
    }
}
```

VERBOSE и DEBUG logs __должны__ быть выключены перед коммитом в основную ветку. Также рекомендуется удалять INFORMATION, WARNING и ERROR logs, но, возможно, по каким-то причинам, ты захочешь оставить их, если ты считаешь, что они могут быть полезными другим девелоперам. Если ты решил оставить логи включенными, убедись, что они не содержат важную или приватную информацию такую, как ФИО пользователей, персональные данные, пароли, емэйлы и т. д.

Чтобы показывать логи только в debug builds:

```
if (BuildConfig.DEBUG) Log.d(TAG, "The value of x is " + x);
```

__Note:__ Для логирования также можно (и нужно) использовать подключенную в проекте библиотеку `Timber`. Работает аналогично стандартному, описанному выше способу.

### 2.2.10 Последовательность членов Класса

Нет единого верного решения для определения правильной последовательности членов класса, но __логический__ и __последовательный__ порядок улучшит читаемость кода. В качестве рекомендации можно использовать следующий порядок:

1. Constants
2. Fields
3. Constructors
4. Override methods and callbacks (public or private)
5. Public methods
6. Private methods
7. Inner classes or interfaces

Пример:

```
public class MainActivity extends BaseActivity {

    private String mTitle;
    private TextView mTextViewTitle;

    public void setTitle(String title) {
        mTitle = title;
    }

    @Override
    public void onCreate() {
        ...
    }

    private void setUpView() {
        ...
    }

    static class AnInnerClass {

    }
}
```

Если твой класс наследуется от __Android компонента__, такого, как Activity или Fragment, то хорошей практикой является определить последовательность переопределяемых методов так, чтобы они __соответствовали жизненному циклу компонента__. Например, если у тебя есть Activity, которая имплементирует `onCreate()`, `onDestroy()`, `onPause()` и `onResume()`, тогда правильной последовательностью будет:

```
public class MainActivity extends Activity {

    //Order matches Activity lifecycle
    @Override
    public void onCreate() {
        ...
    }

    @Override
    public void onResume() {
        ...
    }

    @Override
    public void onPause() {
        ...
    }

    @Override
    public void onDestroy() {
        ...
    }
}
```

### 2.2.11 Последовательность параметров в методах

Программируя для Android, очень часто необходимо определять методы, которые принимают `Context`. Если ты пишешь подобный метод, то __Context__ должен быть __первым__ параметром.

И наоборот, __callback'и__ интерфейсов всегда должны быть __последними__ параметрами.

Examples:

```
// Context always goes first
public User loadUser(Context context, int userId);

// Callbacks always go last
public void loadUserAsync(Context context, int userId, UserCallback callback);
```

### 2.2.12 Формирование "сложных" объектов

Если ты хочешь создать сложный объект или различные его вариации, чтобы не плодить десяток конструкторов, __используй порождающий шаблон Builder__. (Как правильно создавать билдер есть много информации в интернете).

Пример использования:

```
Intent intent = EmailIntentBuilder.from(getContext())
                .to(Config.EMAIL_SUPPORT)
                .subject(subject)
                .body(text)
                .build();
```


### 2.2.13 Константы, названия и значения

__Числовые константы:__

Помни: __недопустимо__ использовать [__magic numbers__](https://ru.wikipedia.org/wiki/%D0%9C%D0%B0%D0%B3%D0%B8%D1%87%D0%B5%D1%81%D0%BA%D0%BE%D0%B5_%D1%87%D0%B8%D1%81%D0%BB%D0%BE_(%D0%BF%D1%80%D0%BE%D0%B3%D1%80%D0%B0%D0%BC%D0%BC%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5)) в коде! Не вынесенная в константы переменная усложняет читаемость кода и является потенциальной причиной ошибки.

__Строковые константы:__

Многие элементы в Android SDK, такие, как `SharedPreferences`, `Bundle`, или `Intent` используют key-value пары, так что достаточно быстро ты столкнешься с тем, что нужно будет писать большое количество String констант.

Используя одну из этих компонент, ты __должен__ определять ключи, как `static final` fields и в них должен использоваться префикс, как показано ниже.

| Element            |  Field Name Prefix  |
| -----------------  | ------------------- |
| SharedPreferences  | `PREF_`             |
| Bundle             | `BUNDLE_`           |
| Fragment Arguments | `ARGUMENT_`         |
| Intent Extra       | `EXTRA_`            |
| Intent Action      | `ACTION_`           |

Помни, что аргументы Fragment'а - `Fragment.getArguments()` - также Bundle. Тем не менее, т. к. это достаточно частый случай использования Bundles, для них мы определяем другие префиксы.

Пример:

```
// Note the value of the field is the same as the name to avoid duplication issues
static final String PREF_EMAIL = "PREF_EMAIL";
static final String BUNDLE_AGE = "BUNDLE_AGE";
static final String ARGUMENT_USER_ID = "ARGUMENT_USER_ID";

// Intent-related items use full package name as value
static final String EXTRA_SURNAME = "com.myapp.extras.EXTRA_SURNAME";
static final String ACTION_OPEN_USER = "com.myapp.action.ACTION_OPEN_USER";
```

### 2.2.14 Аргументы в Fragment'ах и Activities

Когда данные посылаются в `Activity` или `Fragment` через `Intent` или `Bundle`, ключи для различных значений __должны__ следовать правилам, определенным выше.

Когда `Activity` или `Fragment` ожидают аргументы, должен быть создан `public static` метод, который облегчает создание релевантного `Intent` или `Fragment`.

В случае с Activities этот метод обычно называется `getStartIntent()`:

```
public static Intent getStartIntent(Context context, User user) {
    Intent intent = new Intent(context, ThisActivity.class);
    intent.putParcelableExtra(EXTRA_USER, user);
    return intent;
}
```

Для фрагментов он называется `newInstance()` и обеспечивает создание Fragment с правильными аргументами:

```
public static UserFragment newInstance(User user) {
    UserFragment fragment = new UserFragment;
    Bundle args = new Bundle();
    args.putParcelable(ARGUMENT_USER, user);
    fragment.setArguments(args);
    return fragment;
}
```

__Note 1__: Эти методы должны быть расположены в самом верху файла, перед `onCreate()`.

__Note 2__: Если мы предоставляем методы, о которых идет речь выше, то keys для extras и arguments должны быть `private` т. к. нет никакой необходимости использовать их за пределами класса.

### 2.2.15 Лимит длины строки

Длина строк не должна превышать __100 символов__. Если строка длиннее этого лимита, то обычно есть два варианта:

* Выделить локальную переменную или метод (предпочтительно).
* Применить разделение строки, чтобы разделить одну длинную строку на несколько коротких.

Есть два __исключения__, когда возможно наличие строки с более, чем 100 символов:

* Строку невозможно разделить, например, длинный URL в комментарии.
* `package` и `import` statements.

#### 2.2.15.1 Стратегии переноса строк

Нет точной формулы, которая бы объяснила, как правильно разделить строку и очень часто применяются различные решения. Тем не менее, есть несколько правил, которые могут быть применены в распространенных случаях.

__Разрыв строки перед оператором__

Когда строка разрывается оператором, разрыв необходимо применять __перед__ оператором. Например:

```
int longName = anotherVeryLongVariable + anEvenLongerOne - thisRidiculousLongOne
        + theFinalOne;
```

__Исключение в случае с оператором `=`__

Исключением к предыдущему правилу является оператор `=`, когда разрыв строки следует __после__ оператора.

```
int longName =
        anotherVeryLongVariable + anEvenLongerOne - thisRidiculousLongOne + theFinalOne;
```

__Случай связанной цепочки методов__

Когда несколько методов определены в одной строке, например, когда используются Builders, каждый вызов метода должен идти с новой строки, применяя разрыв строки перед `.`

```
Picasso.with(context).load("https://awesomesite.ru/sexyimg.jpg").into(imageView);
```

```
Picasso.with(context)
        .load("https://awesomesite.ru/sexyimg.jpg")
        .into(imageView);
```

__Случай с длинными параметрами__

Когда у метода есть много параметров или его параметры слишком длинные, мы должны разрывать строку после каждой запятой `,`

```
loadPicture(context, "http://awesomesite.ru/sexyimg.jpg", mImageViewProfilePicture, clickListener, "Title of the picture");
```

```
loadPicture(context,
        "http://awesomesite.ru/sexyimg.jpg",
        mImageViewProfilePicture,
        clickListener,
        "Title of the picture");
```

### 2.2.16 RxJava chains styling

Операторы Rx необходимо разделять. Каждый оператор должен идти с новой строки, и строка должна прерываться перед `.`

```
public Observable<Location> syncLocations() {
    return mDatabaseHelper.getAllLocations()
            .concatMap(new Func1<Location, Observable<? extends Location>>() {
                @Override
                 public Observable<? extends Location> call(Location location) {
                     return mRetrofitService.getLocation(location.id);
                 }
            })
            .retry(new Func2<Integer, Throwable, Boolean>() {
                 @Override
                 public Boolean call(Integer numRetries, Throwable throwable) {
                     return throwable instanceof RetrofitError;
                 }
            });
}
```

## 2.3 Правила стилей XML

### 2.3.1 Используй самозакрывающиеся тэги

Когда у XML элемента нет контента, ты __должен__ использовать самозакрывающиеся тэги.

Так делать хорошо:

```
<TextView
    android:id="@+id/text_view_profile"
    android:layout_width="wrap_content"
    android:layout_height="wrap_content" />
```

Так __плохо__ :

```
<!-- Никогда так не делай! -->
<TextView
    android:id="@+id/text_view_profile"
    android:layout_width="wrap_content"
    android:layout_height="wrap_content" >
</TextView>
```


### 2.3.2 Названия Resources

Resource IDs и names пишутся в __lowercase_underscore__.

#### 2.3.2.1 ID naming

Для IDs должны использоваться префиксы с именем элемента в __lowercase_underscore__. Например:

| Element              | Prefix              |
| -------------------- | ------------------- |
| `TextView`           | `text_`             |
| `ImageView`          | `image_`            |
| `Button`             | `button_`           |
| `Menu`               | `menu_`             |

Пример Image view:

```
<ImageView
    android:id="@+id/image_profile"
    android:layout_width="wrap_content"
    android:layout_height="wrap_content" />
```

Пример Menu:

```
<menu>
    <item
        android:id="@+id/menu_done"
        android:title="Done" />
</menu>
```

#### 2.3.2.2 Strings

Названия String начинаются с префикса, который идентифицирует section к которому они принадлежат. Например `registration_email_hint` или `registration_name_hint`. Если String __не принадлежит__ к какой-либо section, то ты должен следовать следующим правилам:

| Префикс              | Описание                                     |
| -----------------    | ---------------------------------------------|
| `error_`             | Сообщение об ошибке                          |
| `msg_`               | Обычное информационное сообщение             |
| `title_`             | Заголовок, например, dialog title            |
| `action_`            | Action, такой как "Сохранить" или "Создать"  |



#### 2.3.2.3 Styles и Themes

Кроме всех __resources__, имена стилей пишутся в __UpperCamelCase__.

Для поддержки разных цветовых тем в приложении, при указании цвета __следует использовать аттрибуты__.
Они должны хранится в файле `theme_attrs.xml` и определены в `styles.xml`, как минимум, для одной темы `AppThemeDefault`.
Получить доступ программно можно с помощью методов из класса `ThemeUtils`, например:

```
@ColorInt
public static int getThemeColor(Context context, @AttrRes int colorAttr)
```

### 2.3.3 Attributes ordering

В качестве основного правила, ты должен стараться группировать подобные аттрибуты вместе. Хороший вариант группировки наиболее распространенных аттрибутов:

1. View Id
2. Style
3. Layout width и layout height
4. Другие layout аттрибуты в алфавитном порядке
5. Оставшиеся аттрибуты в алфавитном порядке

Также не забывай, что ты можешь использовать горячую клавишу `Ctrl + Alt + L` в Андроид Студио для автоматического форматирования XML-файла.

## 2.4 Правила Tests style

### 2.4.1 Unit тесты

Имена классов с тестами должны состоять из слова `Test`, перед которым стоит имя класса, для которого тесты написаны. Например, если мы создаем класс с тестами для `DatabaseHelper`, мы должны назвать его `DatabaseHelperTest`.

Test methods должны содержать аннотацию `@Test` и должны начинаться с названия метода, который они собираются тестировать, после которого следует предварительное условие и/или ожидаемое поведение метода.

* Template: `@Test void methodNamePreconditionExpectedBehaviour()`
* Example: `@Test void signInWithEmptyEmailFails()`

Предварительное условие и/или ожидаемое поведение не всегда являются обязательными, если тест достаточно простой для восприятия и без них.

Иногда класс может содержать большое количество методов, которые в то же время требуют нескольких тестов для каждого метода. В этом случае, рекомендуется разделять Test Classes на несколько. Например, если `DataManager` содержит множество методов, мы можем разделить тесты на `DataManagerSignInTest`, `DataManagerLoadUsersTest`, и т. д. Главное, что ты будешь видеть, какие тесты связаны вместе. [test fixtures](https://en.wikipedia.org/wiki/Test_fixture).
