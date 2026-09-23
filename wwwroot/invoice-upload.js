window.uploadInvoice = async function (invoice) {
    const input = document.getElementById("invoiceFile");
    const form = new FormData();

    form.append("Name", invoice.name);
    form.append("notifyDaysBefore", invoice.notifyDaysBefore);
    form.append("sendEmail", invoice.sendEmail);
    form.append("sendSMS", invoice.sendSMS);
    form.append("PaidAt", invoice.paidAt);
    form.append("CreatedAt", invoice.createdAt);

    if (input?.files?.length > 0) {
        form.append("uploadedInvoice", input.files[0], input.files[0].name);
    }

    const response = await fetch("/api/form/invoice", {
        method: "POST",
        body: form,
        credentials: "same-origin"
    });

    return {
        ok: response.ok,
        status: response.status,
        body: await response.text()
    };
};
