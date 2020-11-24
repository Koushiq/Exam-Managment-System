using ExamManagementSystem.Models.DataAccess;
using ExamManagementSystem.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Repository
{
    public class OptionRepository:Repository<Option>
    {
        public void InsertOptionList(List<OptionJSON> optionJSONList, int questionId)
        {
            foreach(OptionJSON optionJSON in optionJSONList)
            {
                Option option = new Option();
                option.OptionText = optionJSON.OptionText;
                option.OptionId = (short)optionJSON.OptionId;
                option.QuestionId = questionId;

                this.Insert(option);
            }
        }

        public void UpdateOptionList(List<OptionJSON> optionJSONList, int questionId)
        {
            foreach (OptionJSON optionJSON in optionJSONList)
            {
                //Option option = new Option();
                //option.OptionText = optionJSON.OptionText;
                //option.OptionId = (short)optionJSON.OptionId;
                //option.QuestionId = questionId;
                if(optionJSON.Id != null)
                {
                    Option option = this.Get((int)optionJSON.Id);
                    //option.Id = (int)optionJSON.Id;
                    option.OptionText = optionJSON.OptionText;
                    option.OptionId = (short)optionJSON.OptionId;
                    //option.QuestionId = questionId;
                    this.Update(option);
                }
                else
                {
                    Option option = new Option();
                    option.OptionText = optionJSON.OptionText;
                    option.OptionId = (short)optionJSON.OptionId;
                    option.QuestionId = questionId;

                    this.Insert(option);
                }
            }
        }
    }
}