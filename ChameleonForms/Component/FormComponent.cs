﻿using System;
using System.Web;
using ChameleonForms.Templates;

namespace ChameleonForms.Component
{
    public interface IFormComponent<TModel, TTemplate> where TTemplate : IFormTemplate, new()
    {
        Form<TModel, TTemplate> Form { get; }
    }

    public abstract class FormComponent<TModel, TTemplate> : IFormComponent<TModel, TTemplate>, IHtmlString, IDisposable where TTemplate : IFormTemplate, new()
    {
        private readonly bool _isSelfClosing;
        public Form<TModel, TTemplate> Form { get; private set; }

        protected FormComponent(Form<TModel, TTemplate> form, bool isSelfClosing)
        {
            Form = form;
            _isSelfClosing = isSelfClosing;

            if (!_isSelfClosing)
                form.Write(Begin());
        }

        public abstract IHtmlString Begin();
        public abstract IHtmlString End();
        public string ToHtmlString()
        {
            if (_isSelfClosing)
                return null;

            return string.Format("{0}{1}", Begin().ToHtmlString(), End().ToHtmlString());
        }

        public void Dispose()
        {
            if (!_isSelfClosing)
                Form.Write(End());
        }
    }
}