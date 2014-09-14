function partial(func /*, 0..n args */) {
    var args = Array.prototype.slice.call(arguments, 1);
    return function () {
        var allArguments = args.concat(Array.prototype.slice.call(arguments));
        return func.apply(this, allArguments);
    };
}

var arr = [];

function r(a,b) {
    alert(a + b);
}

for (var i = 0; i < 3; i++) {

    var f1 = partial(r,i,i);
    
    arr.push(f1);
    
    f1();
}

