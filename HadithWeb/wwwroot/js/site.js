// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.querySelectorAll('.islamic-card').forEach(card => {
    card.onmousemove = e => {
        const rect = card.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;

        // إنشاء شعاع ضوئي يتبع الماوس داخل البطاقة
        card.style.setProperty('--mouse-x', `${x}px`);
        card.style.setProperty('--mouse-y', `${y}px`);
    };
});
function animateValue(obj, start, end, duration) {
    let startTimestamp = null;
    const step = (timestamp) => {
        if (!startTimestamp) startTimestamp = timestamp;
        const progress = Math.min((timestamp - startTimestamp) / duration, 1);
        obj.innerHTML = Math.floor(progress * (end - start) + start);
        if (progress < 1) {
            window.requestAnimationFrame(step);
        }
    };
    window.requestAnimationFrame(step);
}

// تشغيل العداد عند تحميل الصفحة لعدد النتائج
const countBadge = document.querySelector('.badge.bg-secondary');
if (countBadge) {
    const finalValue = parseInt(countBadge.innerText.replace(/[^0-9]/g, ''));
    animateValue(countBadge, 0, finalValue, 1500);
}
document.addEventListener('mousemove', (e) => {
    const amount = 15; // شدة الحركة
    const x = (e.clientX * amount) / window.innerWidth;
    const y = (e.clientY * amount) / window.innerHeight;

    // تحريك الخلفية بشكل طفيف جداً
    document.body.style.backgroundPosition = `${50 + x}% ${50 + y}%`;
});
document.addEventListener('mousemove', (e) => {
    // حساب موقع الماوس لتحريك الخلفية بنسبة بسيطة
    const x = (e.clientX * 0.01);
    const y = (e.clientY * 0.01);

    // تحريك النقش في الخلفية
    document.body.style.backgroundPosition = `${x}px ${y}px`;
});