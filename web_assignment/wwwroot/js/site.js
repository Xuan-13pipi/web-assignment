
//Photo perview
$('.upload input').on('change', e => {
    const file = e.target.files[0];
    const img = $(e.target).siblings('img');

    img.dataset.src ??= img.src;

    if (file && file.type.startsWith('image/')) {
        img.onload = e => URL.revokeObjectURL(img.src);
        img.src = URL.createObjectURL(file);
    }
    else {
        img.src = img.dataset.src;
        e.target.value = '';
    }
    // Trigger input validation
    $(e.target).valid();
});

