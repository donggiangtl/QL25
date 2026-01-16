/* i need an event when a html tag has a new class
ChatGPT said:
Got it 👍 — you want to detect when a class is added (or removed) from an HTML element.
For that, the best way is to use MutationObserver (native JavaScript API). jQuery itself doesn’t provide a built-in event for class changes.
Here’s a jQuery-friendly way to do it: */
// Watch for class changes on a specific element
function onClassChange(selector, callback) {
    const target = $(selector)[0]; // get the raw DOM element

    const observer = new MutationObserver(function(mutations) {
        mutations.forEach(function(mutation) {
            if (mutation.attributeName === "class") {
                callback($(mutation.target));
            }
        });
    });

    observer.observe(target, {
        attributes: true,
        attributeFilter: ['class']
    });
}

// Example usage:
onClassChange("#myDiv", function(el) {
    console.log("Class changed:", el.attr("class"));
    if (el.hasClass("active")) {
        console.log("Element now has class 'active'");
    }
});
/* 🔹 This code will fire whenever #myDiv gets a new class, or loses one.
🔹 You can adapt it to multiple elements by looping through $(selector).each(...).
________________________________________
👉 Do you need this event only for detecting when a specific class (like active) is added, or for any class change?
You said:
yes! i need the event only for a specific class
ChatGPT said:
Perfect 👍 — if you only care about a specific class (say "active"), you can tweak the MutationObserver to fire only when that class is added or removed: */
function onSpecificClassAdded(selector, className, callback) {
    $(selector).each(function () {
        const target = this;

        const observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                if (mutation.attributeName === "class") {
                    const $el = $(mutation.target);
                    
                    // Run callback only when the class is added
                    if ($el.hasClass(className)) {
                        callback($el);
                    }
                }
            });
        });

        observer.observe(target, {
            attributes: true,
            attributeFilter: ['class']
        });
    });
}

// Example usage:
onSpecificClassAdded("#myDiv", "active", function(el) {
    console.log("✅ The element now has 'active':", el.attr("class"));
});
/* 🔹 The callback will only run when the element gains the class active.
🔹 If you also want to detect when it’s removed, you can add a second check: */
if ($el.hasClass(className)) {
    callback($el, "added");
} else {
    callback($el, "removed");
}
________________________________________
/* 👉 Do you want me to make it handle both add and remove with different event types, or just added is enough?
You said:
give me the code that handle both add and remove the "active" class
ChatGPT said:
Got it ✅ — here’s a clean version that handles both adding and removing of a specific class ("active") and tells you which happened: */
function onSpecificClassChange(selector, className, callback) {
    $(selector).each(function () {
        const target = this;

        const observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                if (mutation.attributeName === "class") {
                    const $el = $(mutation.target);

                    if ($el.hasClass(className)) {
                        callback($el, "added");   // class was added
                    } else {
                        callback($el, "removed"); // class was removed
                    }
                }
            });
        });

        observer.observe(target, {
            attributes: true,
            attributeFilter: ["class"]
        });
    });
}

// Example usage:
onSpecificClassChange("#myDiv", "active", function(el, status) {
    if (status === "added") {
        console.log("✅ 'active' was ADDED to:", el.attr("id"));
    } else {
        console.log("❌ 'active' was REMOVED from:", el.attr("id"));
    }
});
/* 🔹 Works for any selector, even multiple elements.
🔹 The callback gets two arguments:
•	el → the jQuery element
•	status → "added" or "removed"
________________________________________
👉 Do you want me to make this work globally (like for any element that ever gains/removes active), or only for specific elements you target?
You said:
my selector is class and i need to show the data of DOM element when the event fires
ChatGPT said:
Perfect 👍 — if your selector is a class (like .item), and you want to see the DOM data when the "active" class is added or removed, you can use this: */
function onSpecificClassChange(selector, className, callback) {
    $(selector).each(function () {
        const target = this;

        const observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                if (mutation.attributeName === "class") {
                    const $el = $(mutation.target);

                    if ($el.hasClass(className)) {
                        callback($el, "added");
                    } else {
                        callback($el, "removed");
                    }
                }
            });
        });

        observer.observe(target, {
            attributes: true,
            attributeFilter: ["class"]
        });
    });
}

// Example usage:
onSpecificClassChange(".item", "active", function(el, status) {
    if (status === "added") {
        console.log("✅ 'active' ADDED:", el.data());   // show jQuery data-* attributes
    } else {
        console.log("❌ 'active' REMOVED:", el.data());
    }
});
/* Example HTML:
<div class="item" data-id="101" data-name="Apple">Apple</div>
<div class="item" data-id="102" data-name="Banana">Banana</div>
Console Output:
✅ 'active' ADDED: {id: "101", name: "Apple"}
❌ 'active' REMOVED: {id: "102", name: "Banana"}
________________________________________
👉 Do you want me to also include the element’s text / innerHTML along with its data-* attributes when logging, or just the data-* attributes?
You said:
i need show only the name of tag
ChatGPT said:
Got it 👍 — if you only want to show the tag name (like DIV, SPAN, LI …) when the "active" class is added/removed, you can do this: */
function onSpecificClassChange(selector, className, callback) {
    $(selector).each(function () {
        const target = this;

        const observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                if (mutation.attributeName === "class") {
                    const $el = $(mutation.target);

                    if ($el.hasClass(className)) {
                        callback($el, "added");
                    } else {
                        callback($el, "removed");
                    }
                }
            });
        });

        observer.observe(target, {
            attributes: true,
            attributeFilter: ["class"]
        });
    });
}

// Example usage:
onSpecificClassChange(".item", "active", function(el, status) {
    console.log(`Tag <${el.prop("tagName").toLowerCase()}>: ${status}`);
});
/* Example HTML:
<div class="item">Apple</div>
<span class="item">Banana</span>
<li class="item">Cherry</li>
Console Output:
Tag <div>: added
Tag <span>: removed
Tag <li>: added
________________________________________
👉 Do you also want me to show the text content of the tag (like Apple, Banana) along with the tag name, or only the tag name itself?
You said:
no, i mean, i need to show only the data-name of tag
ChatGPT said:
Ah 👍 got it — you don’t want the tag name itself, but the data-name attribute of the element.
Here’s the adjusted code: */
function onSpecificClassChange(selector, className, callback) {
    $(selector).each(function () {
        const target = this;

        const observer = new MutationObserver(function (mutations) {
            mutations.forEach(function (mutation) {
                if (mutation.attributeName === "class") {
                    const $el = $(mutation.target);

                    if ($el.hasClass(className)) {
                        callback($el, "added");
                    } else {
                        callback($el, "removed");
                    }
                }
            });
        });

        observer.observe(target, {
            attributes: true,
            attributeFilter: ["class"]
        });
    });
}

// Example usage:
onSpecificClassChange(".item", "active", function(el, status) {
    console.log(`data-name="${el.data("name")}" was ${status}`);
});
/* Example HTML:
<div class="item" data-id="101" data-name="Apple">Apple</div>
<div class="item" data-id="102" data-name="Banana">Banana</div>
Console Output:
data-name="Apple" was added
data-name="Banana" was removed
________________________________________
👉 Do you want me to make it so the event listens automatically for any new .item elements added dynamically to the DOM, or only for the ones that already exist when the page loads?
You said:
can i define my custom data instead of only "id", "name" ...
ChatGPT said:
Absolutely ✅ — you can define any custom data-* attribute you want on your HTML tags.
HTML5 allows arbitrary attributes as long as they start with data-.
Example HTML with custom data:
<div class="item" data-id="101" data-name="Apple" data-price="10.5" data-color="red">Apple</div>
<div class="item" data-id="102" data-name="Banana" data-price="6.2" data-color="yellow">Banana</div>
Accessing them in jQuery: */
$(".item").each(function() {
    let $el = $(this);

    console.log($el.data("id"));     // 101
    console.log($el.data("name"));   // Apple
    console.log($el.data("price"));  // 10.5
    console.log($el.data("color"));  // red
});
/* So in your class-change event watcher, you can log any of them: */
onSpecificClassChange(".item", "active", function(el, status) {
    console.log(
        `Fruit: ${el.data("name")} | Price: ${el.data("price")} | Status: ${status}`
    );
});
/* Example Console Output:
Fruit: Apple | Price: 10.5 | Status: added
Fruit: Banana | Price: 6.2 | Status: removed */

